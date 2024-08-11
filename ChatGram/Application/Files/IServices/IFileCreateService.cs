using Application.Files.Results;
using Application.Responses;
using Domain.ILifeTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Files.IServices
{
    public interface IFileCreateService : IScopedService
    {
        Task<ServiceResponse<FileCreateServiceResult>> CreateFile(string sessionId);
    }
}
