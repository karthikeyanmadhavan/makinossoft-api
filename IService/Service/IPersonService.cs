using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace IService
{
    public interface IPersonService
    {
        public Task<byte[]> Download(string filePath);
        public Task<bool> Save(string filePath, PersonModel personModel);
        public Task<List<PersonModel>> List(string filePath);
    }
}
