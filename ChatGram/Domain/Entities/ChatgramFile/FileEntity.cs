using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ChatgramFile
{
    public class FileEntity
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long FileSize { get; set; }
        public byte[] Data { get; set; }
        public DateTime DateUploaded { get; set; }
        public bool IsDeleted { get; set; }
        public string FileUrl { get; set; }
        public string Description { get; set; }
        public Guid RowGuid { get; set; }
    }


}
