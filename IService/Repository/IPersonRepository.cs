using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IService
{
    public interface IPersonRepository
    {
        Task<byte[]> Download(string filePath);
        Task<bool> Save(string filePath, List<Person> persons);
        Task<List<Person>> List(string filePath);
    }
}
