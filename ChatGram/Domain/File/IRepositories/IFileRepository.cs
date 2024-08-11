using Domain.Entities.ChatgramFile;
using Domain.ILifeTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.File.IRepositories
{
    public interface IFileRepository : ITransientService
    {
        Task AddFileAsync(FileEntity fileEntity);
    }
}
