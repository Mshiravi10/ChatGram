using Domain.Entities.ChatgramFile;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ChatgramFileDbContext : DbContext
    {
        public ChatgramFileDbContext(DbContextOptions<ChatgramFileDbContext> options)
            : base(options)
        {
        }

        public DbSet<FileEntity> Files { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FileEntity>(entity =>
            {
                // مشخص کردن نام جدول
                entity.ToTable("Files");

                // تنظیم کردن Id به عنوان کلید اصلی
                entity.HasKey(e => e.Id);

                // تنظیم کردن ستون Id
                entity.Property(e => e.Id)
                      .HasColumnName("Id")
                      .HasColumnType("uniqueidentifier")
                      .HasDefaultValueSql("NEWID()")
                      .IsRequired()
                      .ValueGeneratedOnAdd();

                // تنظیم کردن ستون RowGuid
                entity.Property(e => e.RowGuid)
                      .HasColumnName("RowGuid")
                      .HasColumnType("uniqueidentifier")
                      .HasDefaultValueSql("NEWID()")
                      .IsRequired();

                // ایجاد ایندکس برای ستون RowGuid
                entity.HasIndex(e => e.RowGuid)
                      .IsUnique();

                // تنظیم ستون‌های دیگر
                entity.Property(e => e.FileName)
                      .IsRequired()
                      .HasColumnType("nvarchar(max)");

                entity.Property(e => e.ContentType)
                      .IsRequired()
                      .HasColumnType("nvarchar(max)");

                entity.Property(e => e.FileSize)
                      .IsRequired()
                      .HasColumnType("bigint");

                entity.Property(e => e.Data)
                      .IsRequired()
                      .HasColumnType("varbinary(max) FILESTREAM");

                entity.Property(e => e.DateUploaded)
                      .IsRequired()
                      .HasColumnType("datetime2");

                entity.Property(e => e.IsDeleted)
                      .IsRequired()
                      .HasColumnType("bit");

                entity.Property(e => e.FileUrl)
                      .HasColumnType("nvarchar(max)");

                entity.Property(e => e.Description)
                      .HasColumnType("nvarchar(max)");
            });

            base.OnModelCreating(modelBuilder);
        }








    }

}
