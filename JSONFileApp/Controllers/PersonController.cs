using JSONFileAPI.Common;

namespace JSONFileAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ApiResultController
    {
        private string filePath { get; set; }
        private IPersonService personService { get; set; }
        public PersonController(IPersonService _personService, IConfiguration config)
        {
            personService = _personService;
            filePath = config.GetValue<string>("PersonFile");
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult> List()
        {
            List<PersonModel> personsData = await personService.List(filePath);
            return Ok(personsData);
        }

        [HttpGet]
        [Route("download")]
        public async Task<FileResult> Download()
        {
            byte[] personsData = await personService.Download(filePath);
            return File(personsData, "application/json", filePath);
        }

        [HttpPost]
        [Route("save")]
        public async Task<ActionResult> Save([FromBody] PersonModel personModel)
        {
            bool result = await personService.Save(filePath, personModel);
            return Ok(result);
        }
    }
}
