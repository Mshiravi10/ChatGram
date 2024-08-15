using Application.Chache.IService;
using Application.Files;
using Application.Files.Commands;
using Application.Files.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.V1.FileUpload
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IRedisCacheService _sessionService;

        public FileUploadController(IRedisCacheService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpPost("start-upload")]
        public async Task<IActionResult> StartUpload([FromBody] StartUploadCommand command)
        {
            if(string.IsNullOrEmpty(command.FileName) || command.ChunkSize <= 0 || command.TotalSize <= 0)
            {
                return BadRequest("Invalid request");
            }

            var sessionId = Guid.NewGuid().ToString();

            var session = new FileUploadSession
            {
                SessionId = sessionId,
                FileName = command.FileName,
                ChunkSize = command.ChunkSize,
                TotalSize = command.TotalSize,
                CurrentChunk = 0,
                IsCompleted = false
            };

            await _sessionService.SetAsync(sessionId, session, TimeSpan.FromHours(1));

            return Ok(new { sessionId });

        }

        [HttpPost("upload-chunk")]
        public async Task<IActionResult> UploadChunk([FromForm] UploadChunkCommand command)
        {
            if (string.IsNullOrEmpty(command.SessionId) || command.ChunkIndex < 0 || command.ChunkFile == null || command.ChunkFile.Length == 0)
            {
                return BadRequest("Invalid chunk data");
            }

            var session = await _sessionService.GetAsync<FileUploadSession>(command.SessionId);
            if (session == null)
            {
                return NotFound("Upload session not found");
            }

            FilePaths.EnsureDirectoriesExist();

            string tempFilePath = FilePaths.GetTempFilePath(session.SessionId);
            using (var stream = new FileStream(tempFilePath, FileMode.Append))
            {
                await command.ChunkFile.CopyToAsync(stream);
            }

            session.CurrentChunk = command.ChunkIndex + 1;

            if (session.CurrentChunk * session.ChunkSize >= session.TotalSize)
            {
                session.IsCompleted = true;
            }

            await _sessionService.SetAsync(session.SessionId, session);

            return Ok();
        }

        [HttpPost("complete-upload")]
        public async Task<IActionResult> CompleteUpload([FromBody] CompleteUploadCommand command)
        {
            var session = await _sessionService.GetAsync<FileUploadSession>(command.SessionId);
            if (session == null)
            {
                return NotFound("Upload session not found");
            }

            if (!session.IsCompleted)
            {
                return BadRequest("Upload is not yet completed");
            }

            FilePaths.EnsureDirectoriesExist();

            string tempFilePath = FilePaths.GetTempFilePath(session.SessionId);
            string finalFilePath = FilePaths.GetFinalFilePath(session.FileName);

            if (System.IO.File.Exists(finalFilePath))
            {
                System.IO.File.Delete(finalFilePath);
            }

            System.IO.File.Move(tempFilePath, finalFilePath);


            return Ok(new { FilePath = finalFilePath });
        }

    }
}
