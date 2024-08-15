using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Files.Commands
{
    public class StartUploadCommand
    {
        public string FileName { get; set; }      // نام فایل
        public long ChunkSize { get; set; }       // اندازه هر chunk (بایت)
        public long TotalSize { get; set; }       // اندازه کل فایل (بایت)

    }
}
