using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Files
{
    public static class FilePaths
    {
        public static readonly string BaseDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

        public static string TempDirectory => Path.Combine(BaseDirectory, "Temp");

        public static string FinalDirectory => Path.Combine(BaseDirectory, "Final");

        public static string GetTempFilePath(string sessionId) => Path.Combine(TempDirectory, sessionId);

        public static string GetFinalFilePath(string fileName) => Path.Combine(FinalDirectory, fileName);

        public static void EnsureDirectoriesExist()
        {
            Directory.CreateDirectory(TempDirectory);
            Directory.CreateDirectory(FinalDirectory);
        }
    }

}
