using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IService
{
    public interface IPersonRepository
    {
        public Task<byte[]> Download(string filePath);
        public Task<bool> Save(string filePath, List<Person> persons);
        public Task<List<Person>> List(string filePath);
    }
}
