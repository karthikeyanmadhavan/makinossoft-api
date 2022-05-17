using System;
using System.IO;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class FileAccess : IFileAccess
    {
        public async Task<byte[]> Download(string filePath)
        {
            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);
            if (File.Exists(filePath))
            {
                return await File.ReadAllBytesAsync(filePath);
            }
            return null;
        }

        public async Task<string> Read(string filePath)
        {
            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);
            if (File.Exists(filePath))
            {
                return await File.ReadAllTextAsync(filePath);
            }
            return string.Empty;
        }

        public async Task<bool> Save(string filePath, string dataString)
        {
            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);
            if (!File.Exists(filePath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }
            await File.WriteAllTextAsync(filePath, dataString);
            return true;
        }
    }
}
