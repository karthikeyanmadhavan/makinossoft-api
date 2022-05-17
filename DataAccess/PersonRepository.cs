using DataAccess.Data;
using Domain;
using IService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess
{
    public class PersonRepository : IPersonRepository
    {
        private IFileAccess fileAccess;
        public PersonRepository(IFileAccess _fileAccess)
        {
            fileAccess = _fileAccess;
        }

        public async Task<byte[]> Download(string filePath)
        {
            return await fileAccess.Download(filePath);
        }

        public async Task<List<Person>> List(string filePath)
        {
            string jsonData = await fileAccess.Read(filePath);
            return JsonConvert.DeserializeObject<List<Person>>(jsonData);
        }

        public async Task<bool> Save(string filePath, List<Person> persons)
        {
            return await fileAccess.Save(filePath, JsonConvert.SerializeObject(persons));
        }
    }
}
