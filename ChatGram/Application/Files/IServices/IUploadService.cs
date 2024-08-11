using Application.Files.DTOs;
using Domain.ILifeTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Files.IServices
{
    public interface IUploadService : IScopedService
    {
        public Task<FileUploadSession> GetFileSession(string sessionId);
        public Task<byte[]> GetFileBytes(string sessionId);
    }
}
