using Application.Files.IServices;
using Application.Files.Results;
using Application.Responses;
using Domain.Entities.ChatgramFile;
using Domain.File.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Files.Services
{
    public class FileCreateService : IFileCreateService
    {
        private readonly IFileRepository fileRepository;
        private readonly IUploadService uploadService;

        public FileCreateService(IFileRepository fileRepository, IUploadService uploadService)
        {
            this.fileRepository = fileRepository;
            this.uploadService = uploadService;
        }

        public async Task<ServiceResponse<FileCreateServiceResult>> CreateFile(string sessionId)
        {
            var fileSession = await uploadService.GetFileSession(sessionId);
            var contentType = MimeMapping.MimeUtility.GetMimeMapping(fileSession.FileName);
            var fileData = await uploadService.GetFileBytes(sessionId);
            var fileEntity = new FileEntity
            {
                ContentType = contentType,
                DateUploaded = DateTime.Now,
                FileName = fileSession.FileName,
                FileSize = fileSession.TotalSize,
                Data = fileData,
                RowGuid = Guid.NewGuid(),
                IsDeleted = false,
                
            };

            await fileRepository.AddFileAsync(fileEntity);

            return new ServiceResponse<FileCreateServiceResult>
            {
                Data = new FileCreateServiceResult
                {
                    Guid = fileEntity.Id,
                    FileName = fileEntity.FileName,
                }
            };
        }
    }
}
