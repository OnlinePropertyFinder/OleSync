using Microsoft.AspNetCore.Http;

namespace OleSync.Domain.Shared.Services
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile file, string subDirectory = "uploads");
        bool DeleteFileAsync(string filePath);
        string GetContentType(string fileName);
    }
}