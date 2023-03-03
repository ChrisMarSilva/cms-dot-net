using AwesomeDevEvents.API.Entities;
using AwesomeDevEvents.API.Persistence;
using AwesomeDevEvents.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

            var devEvents = await _context
              .DevEvents
              .AsNoTracking()
              //.Include(d => d.Speakers)
              .Where(d => !d.IsDeleted)
              .OrderByDescending(d => d.StartDate)
              .ToListAsync();
            // var devEvent = await _eventRepository.FindAll();

            return Ok(devEvents);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventsController.GetById()");

            var devEvent = await _context
                .DevEvents
                .AsNoTracking()
                .Include(d => d.Speakers)
                .SingleOrDefaultAsync(d => d.Id == id); 

            //If your result set returns 0 records:
            //SingleOrDefault returns the default value for the type(e.g. default for int is 0)
            //FirstOrDefault returns the default value for the type

            //If you result set returns 1 record:
            //SingleOrDefault returns that record
            //FirstOrDefault returns that record

            //If your result set returns many records:
            //SingleOrDefault throws an exception
            //FirstOrDefault returns the first record

            // var devEvent = await _eventRepository.FindById(id);

            return devEvent?.Id == Guid.Empty ? NotFound() : Ok(devEvent);
        }

        // [ActionName("Details")]
        [HttpGet("find/{id}")]
        public async Task<IActionResult> Get2ById(Guid id)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventsController.Get2ById()");

            var devEvent = await _context
                .DevEvents
                .AsNoTracking()
                .Include(d => d.Speakers)
                .FirstOrDefaultAsync(d => d.Id == id);

            //If your result set returns 0 records:
            //SingleOrDefault returns the default value for the type(e.g. default for int is 0)
            //FirstOrDefault returns the default value for the type

            //If you result set returns 1 record:
            //SingleOrDefault returns that record
            //FirstOrDefault returns that record

            //If your result set returns many records:
            //SingleOrDefault throws an exception
            //FirstOrDefault returns the first record

            // var devEvent = await _eventRepository.FindById(id);

            return devEvent?.Id == Guid.Empty ? NotFound() : Ok(devEvent);
        }

        [HttpPost]
        public async Task<IActionResult> Post(DevEvent input)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventsController.Post()");

            // var result = await _eventRepository.Create(devEvent);
            await _context.DevEvents.AddAsync(input);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = input.Id }, input);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, DevEvent input)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventsController.Update()");

            var devEvent = await _context
                .DevEvents
                .SingleOrDefaultAsync(d => d.Id == id);

            if (devEvent == null)
                return NotFound();

            devEvent.Update(title: input.Title, description: input.Description);

            // var result = await _eventRepository.Update(devEvent);
            _context.DevEvents.Update(devEvent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventsController.Delete()");

            var devEvent = await _context.DevEvents.SingleOrDefaultAsync(d => d.Id == id);

            if (devEvent == null)
                return NotFound();

            // var status = await _eventRepository.Delete(id);
            //if (!status)
            //    return BadRequest();

            devEvent.Delete();
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("{id}/speakers")]
        public async Task<IActionResult> PostSpeaker(Guid id, DevEventSpeaker input)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventsController.PostSpeaker()");

            var isExistDevEvent = await _context.DevEvents.AnyAsync(d => d.Id == id);

            if (!isExistDevEvent)
                return NotFound();

            input.DevEventId = id;

            // var result = await _speakerRepository.Create(input);
            await _context.DevEventSpeakers.AddAsync(input);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
