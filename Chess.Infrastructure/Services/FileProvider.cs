using System;
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
            await Task.CompletedTask;
        }

        // public async Task <string> GetFileContent (string filePath, string fileName)
        // {
        //     String allFilePath = filePath + fileName;
        //     Char[] buffer;

        //     using (var sr = new StreamReader(allFilePath)) {
        //         buffer = new Char[(int)sr.BaseStream.Length];
        //         await sr.ReadAsync(buffer, 0, (int)sr.BaseStream.Length);
        //     }
        //     String aa = new String(buffer);
        //     return aa;
        // }

        public async Task <string> GetFileContent (string filePath, string fileName)
        {
            String allFilePath = filePath + fileName;
            try
            {
            // Open the text file using a stream reader.
                using (var sr = new StreamReader(allFilePath))
                {
                    // Read the stream as a string, and write the string to the console.
                    var aa = sr.ReadToEndAsync();
                    return await aa;
                }
            }
            catch (IOException e)
            {
                throw new Exception("The file could not be read: " + e.Message);
            }
        }
    }
}