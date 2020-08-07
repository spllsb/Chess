using System.Threading.Tasks;

namespace Chess.Infrastructure.Services
{
    public interface IFileProvider : IService
    {
        Task SaveFile(string filePath, string fileName, string content);
    }
}