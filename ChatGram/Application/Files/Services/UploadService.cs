using Application.Chache.IService;
using Application.Files.DTOs;
using Application.Files.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Files.Services
{
    public class UploadService : IUploadService
    {
        private readonly IRedisCacheService redisCacheService;

        public UploadService(IRedisCacheService redisCacheService)
        {
            this.redisCacheService = redisCacheService;
        }

        public async Task<byte[]> GetFileBytes(string sessionId)
        {
            var session = await GetFileSession(sessionId);
            if (session == null)
            {
                throw new FileNotFoundException("Upload session not found.");
            }

            string finalFilePath = FilePaths.GetFinalFilePath(session.FileName);

            if (!System.IO.File.Exists(finalFilePath))
            {
                throw new FileNotFoundException("File not found at the final path.");
            }

            return await System.IO.File.ReadAllBytesAsync(finalFilePath);
        }

        public async Task<FileUploadSession> GetFileSession(string sessionId)
        { 
            return await redisCacheService.GetAsync<FileUploadSession>(sessionId);
        }
    }
}
