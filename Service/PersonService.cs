using AgileObjects.AgileMapper;
using Common;
using Domain;
using IService;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Service
{
    public class PersonService : IPersonService
    {
        private IPersonRepository personRepository;
        public PersonService(IPersonRepository _personRepository)
        {
            personRepository = _personRepository;
        }

        public async Task<byte[]> Download(string filePath)
        {
            var bytes = await personRepository.Download(filePath);
            if (bytes == null || bytes.Count() == 0)
            {
                // 10001 - err code denotes, no data file found.
                throw new ApiException(10001);
            }
            return bytes;
        }
        public async Task<List<PersonModel>> List(string filePath)
        {
            List<Person> persons = await personRepository.List(filePath);
            List<PersonModel> personsModel = Mapper.Map(persons).ToANew<List<PersonModel>>();
            return personsModel;
        }

        public async Task<bool> Save(string filePath, PersonModel personModel)
        {
            Person personEntity = Mapper.Map(personModel).ToANew<Person>();
            List<Person> persons = await personRepository.List(filePath);
            if(persons == null)
            {
                persons = new List<Person>();
            }
            if(persons.Any(s => s.FirstName == personModel.FirstName && s.LastName == personModel.LastName))
            {
                // 10000 - err code denotes, there is data conflict.
                throw new ApiException(10000);
            }
            persons.Add(personEntity);
            return await personRepository.Save(filePath, persons);
        }
    }
}
