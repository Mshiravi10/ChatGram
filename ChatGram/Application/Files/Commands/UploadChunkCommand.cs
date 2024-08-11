using Application.Attributes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Files.Commands
{
    public class UploadChunkCommand
    {
        [Required]
        public string SessionId { get; set; }
        [Required]
        public int ChunkIndex { get; set; }
        [Required]
        [AllowedMimeTypes(new string[] { "image/jpeg", "image/png" })]
        public IFormFile ChunkFile { get; set; }
    }

}
