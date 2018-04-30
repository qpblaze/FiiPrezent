using System;
using System.IO;
using System.Threading.Tasks;
using FiiPrezent.Core.Interfaces;
using Microsoft.AspNetCore.Http;

namespace FiiPrezent.Infrastructure.Services
{
    public class FileManager : IFileManager
    {
        public void Delete(string path)
        {
            string fullPath = Directory.GetCurrentDirectory() + @"\wwwroot\" + path;
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        public async Task<string> UploadAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            var relativePath = Path.Combine(
                                    "uploads",
                                    Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return @"\" + relativePath;
        }
    }
}