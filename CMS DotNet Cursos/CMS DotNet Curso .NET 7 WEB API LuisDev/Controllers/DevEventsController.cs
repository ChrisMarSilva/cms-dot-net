using AwesomeDevEvents.API.Entities;
using AwesomeDevEvents.API.Persistence;
using AwesomeDevEvents.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeDevEvents.API.Controllers
{
    // [Route("api/[controller]")]
    [Route("api/v1/dev-events")]
    [ApiController]
    public class DevEventsController : ControllerBase
    {
        private readonly ILogger<DevEventsController> _logger;
        private readonly ApplicationDbContext _context;
        private IDevEventRepository _eventRepository;
        private IDevEventSpeakerRepository _speakerRepository;

        public DevEventsController(
            ILogger<DevEventsController> logger,
            ApplicationDbContext context,
            IDevEventRepository eventRepository,
            IDevEventSpeakerRepository speakerRepository
            )
        {
            _logger = logger;
            _context = context;            
            _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(DevEventRepository));
            _speakerRepository = speakerRepository ?? throw new ArgumentNullException(nameof(DevEventSpeakerRepository));
            _logger.LogInformation("AwesomeDevEvents.API.DevEventsController");
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventsController.GetAll()");

            var devEvents = await _eventRepository.FindAll();
            return Ok(devEvents);
        }

        // [ActionName("Details")]
        //[HttpGet("find/{id}")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventsController.GetById()");

            var devEvent = await _eventRepository.FindById(id);
            return devEvent?.id == Guid.Empty ? NotFound() : Ok(devEvent);
        }

        [HttpPost]
        public async Task<IActionResult> Post(DevEvent input)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventsController.Post()");
            
            var devEvent = await _eventRepository.Create(input);
            return CreatedAtAction(nameof(GetById), new { id = devEvent.id }, devEvent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, DevEvent input)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventsController.Update()");

            var devEvent = await _eventRepository.FindByIdSimple(id);

            if (devEvent == null || devEvent?.Id == Guid.Empty)
                return NotFound();

            devEvent.Update(title: input.Title, description: input.Description);

            await _eventRepository.Update(devEvent);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventsController.Delete()");

            var status = await _eventRepository.Delete(id);
            
            if (!status)
                return BadRequest();

            return NoContent();
        }

        [HttpPost("{id}/speakers")]
        public async Task<IActionResult> PostSpeaker(Guid id, DevEventSpeaker input)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventsController.PostSpeaker()");

            var isExistDevEvent = await _eventRepository.FindAny(id);
            if (!isExistDevEvent)
                return NotFound();

            input.DevEventId = id;
            await _speakerRepository.Create(input);

            return NoContent();
        }
    }
}
