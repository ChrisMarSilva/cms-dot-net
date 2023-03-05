using AwesomeDevEvents.API.Models;
using AwesomeDevEvents.API.Persistence;
using AwesomeDevEvents.API.Persistence.Interfaces;
using AwesomeDevEvents.API.Repositories;
using AwesomeDevEvents.API.Repositories.Interfaces;
using AwesomeDevEvents.API.ViewModels;
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
        private IDevEventRepository _eventRepo;
        private IDevEventSpeakerRepository _speakerRepo;
        private IUnitofWork _uow; // _unitOfWork

        public DevEventsController(
            ILogger<DevEventsController> logger,
            ApplicationDbContext context,
            IDevEventRepository eventRepository,
            IDevEventSpeakerRepository speakerRepository, 
            IUnitofWork uow
            )
        {
            _logger = logger;
            _context = context;            
            _eventRepo = eventRepository ?? throw new ArgumentNullException(nameof(DevEventRepository));
            _speakerRepo = speakerRepository ?? throw new ArgumentNullException(nameof(DevEventSpeakerRepository));
            _uow = uow;
            _logger.LogInformation("AwesomeDevEvents.API.DevEventsController");
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventsController.GetAll()");

            var devEvents = await _eventRepo.FindAllAsync();
            return Ok(devEvents);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventsController.GetById()");

            var devEvent = await _eventRepo.FindByIdAsync(id);
            return devEvent?.id == Guid.Empty ? NotFound() : Ok(devEvent);
        }

        [HttpPost]
        public async Task<IActionResult> Post(DevEventInput input)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventsController.Post()");
            
            var devEvent = await _eventRepo.CreateAsync(input);
            await _uow.CommitAsync();

            return CreatedAtAction(nameof(GetById), new { id = devEvent.id }, devEvent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, DevEventInput input)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventsController.Update()");

            var devEvent = await _eventRepo.FindByIdSimpleAsync(id);

            if (devEvent == null || devEvent?.Id == Guid.Empty)
                return NotFound();

            devEvent.Update(title: input.title, description: input.description);

            await _eventRepo.UpdateAsync(devEvent);
            await _uow.CommitAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventsController.Delete()");

            var status = await _eventRepo.DeleteAsync(id);
            
            if (!status)
                return BadRequest();

            await _uow.CommitAsync();

            return NoContent();
        }

        [HttpPost("{id}/speakers")]
        public async Task<IActionResult> PostSpeaker(Guid id, DevEventSpeakerInput input)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventsController.PostSpeaker()");

            var isExistDevEvent = await _eventRepo.FindAnyAsync(id);
            if (!isExistDevEvent)
                return NotFound();

            input.DevEventId = id;
            await _speakerRepo.CreateAsync(input);

            return NoContent();
        }
    }
}
