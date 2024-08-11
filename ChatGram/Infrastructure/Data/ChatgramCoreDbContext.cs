using Domain.Entities.ChatgramCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ChatgramCoreDbContext : IdentityDbContext<UserEntity>
    {
        public ChatgramCoreDbContext(DbContextOptions<ChatgramCoreDbContext> options)
            : base(options)
        {
        }

    }
}
