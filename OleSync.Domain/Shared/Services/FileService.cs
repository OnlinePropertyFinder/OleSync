using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace OleSync.Domain.Shared.Services
{
    public class FileService : IFileService
    {
        private readonly IHostEnvironment _environment;
        public FileService(IHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> SaveFileAsync(IFormFile file, string subDirectory = "uploads")
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty");

            // Create directory if it doesn't exist
            var uploadsDir = Path.Combine(_environment.ContentRootPath, "wwwroot", subDirectory);
            if (!Directory.Exists(uploadsDir))
                Directory.CreateDirectory(uploadsDir);

            // Generate unique file name
            var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
            var filePath = Path.Combine(uploadsDir, fileName);

            // Save file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Return relative path for database storage
            return Path.Combine(subDirectory, fileName).Replace("\\", "/");
        }

        public bool DeleteFileAsync(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return false;

            var fullPath = Path.Combine(_environment.ContentRootPath, "wwwroot", filePath);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return true;
            }
            return false;
        }

        public string GetContentType(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return extension switch
            {
                ".pdf" => "application/pdf",
                _ => "application/octet-stream"
            };
        }
    }
}
