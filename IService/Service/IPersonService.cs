using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IService
{
    public interface IPersonService
    {
        Task<byte[]> Download(string filePath);
        Task<bool> Save(string filePath, PersonModel personModel);
        Task<List<PersonModel>> List(string filePath);
    }
}
