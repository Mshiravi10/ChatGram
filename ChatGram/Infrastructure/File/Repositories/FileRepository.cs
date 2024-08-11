using Domain.Entities.ChatgramFile;
using Domain.File.IRepositories;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.File.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly ChatgramFileDbContext _dbContext;

        public FileRepository(ChatgramFileDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddFileAsync(FileEntity fileEntity)
        {
            _dbContext.Files.Add(fileEntity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
