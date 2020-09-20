using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Chess.Infrastructure.Services
{
    public interface IFileProvider : IService
    {
        Task SaveFile(string filePath, string fileName, string content);

        Task <string> GetFileContent(string filePath, string fileName);
        Task <string> UploadedFile(IFormFile profileImage, string uploadsFolder);
    }
}