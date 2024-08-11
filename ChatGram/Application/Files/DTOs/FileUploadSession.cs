using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Files.DTOs
{
    public class FileUploadSession
    {
        public string SessionId { get; set; }          // شناسه یکتای session
        public string FileName { get; set; }           // نام فایل
        public long ChunkSize { get; set; }            // اندازه هر chunk (بایت)
        public long TotalSize { get; set; }            // اندازه کل فایل (بایت)
        public int CurrentChunk { get; set; }         // شماره chunk فعلی
        public List<int> UploadedChunks { get; set; }  // لیست chunkهای آپلود شده
        public bool IsCompleted { get; set; }          // آیا آپلود کامل شده است؟
    }
}
