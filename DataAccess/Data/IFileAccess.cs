using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public interface IFileAccess
    {
        public Task<byte[]> Download(string filePath);
        public Task<bool> Save(string filePath, string dataString);
        public Task<string> Read(string filePath);
    }
}
