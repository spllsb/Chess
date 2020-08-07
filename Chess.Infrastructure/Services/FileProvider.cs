using System.IO;
using System.Threading.Tasks;

namespace Chess.Infrastructure.Services
{
    public class FileProvider : IFileProvider
    {

        public async Task SaveFile(string filePath, string fileName, string content)
        {
            string path = filePath + fileName;

            FileInfo info = new FileInfo(path);
            if (!info.Exists)
            {
                using (StreamWriter writer = info.CreateText())
                {
                    writer.WriteLine(content);
                }
            }
        }
    }
}