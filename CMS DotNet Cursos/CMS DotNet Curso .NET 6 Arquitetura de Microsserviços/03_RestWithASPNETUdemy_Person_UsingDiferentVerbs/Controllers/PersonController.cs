using Microsoft.AspNetCore.Mvc;
using RestWithASPNETUdemy_Calculator.Model;
using RestWithASPNETUdemy_Calculator.Services;

namespace RestWithASPNETUdemy_Calculator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private IPersonService _personService;

        public PersonController(ILogger<PersonController> logger, IPersonService personService)
        {
            _logger = logger;
            _personService = personService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogInformation("GetAll");

            var persons = _personService.FindAll();

            //if (persons == null || !persons.Any())
            //    return NotFound();

            return Ok(persons);
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(long id)
        {
            _logger.LogInformation("GetByID");

            var person = _personService.FindByID(id);
            if (person == null) 
                return NotFound();

            return Ok(person);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Person person)
        {
            _logger.LogInformation("Post");

            if (person == null) 
                return BadRequest();

            person = _personService.Create(person);
            return Ok(person);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Person person)
        {
            _logger.LogInformation("Create");

            if (person == null) 
                return BadRequest();

            person = _personService.Update(person);
            return Ok(person);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _logger.LogInformation("Delete");

            _personService.Delete(id);
            return NoContent();
        }
    }
}
