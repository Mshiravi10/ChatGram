using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Files.Results
{
    public class FileCreateServiceResult
    {
        public Guid Guid { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileExtension { get; set; }
        public string FileSize { get; set; }
        public string FileContentType { get; set; }
        public string FileUrl { get; set; }
    }
}
