using System.Threading.Tasks;

namespace DataAccess.Data
{
    public interface IFileAccess
    {
        Task<byte[]> Download(string filePath);
        Task<bool> Save(string filePath, string dataString);
        Task<string> Read(string filePath);
    }
}
