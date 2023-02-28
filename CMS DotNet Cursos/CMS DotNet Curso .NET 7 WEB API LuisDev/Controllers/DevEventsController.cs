using AwesomeDevEvents.API.Entities;
using AwesomeDevEvents.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AwesomeDevEvents.API.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/v1/dev-events")]
    [ApiController]
    public class DevEventsController : ControllerBase
    {
        private readonly ILogger<DevEventsController> _logger;
        private readonly DevEventsDbContext _context;

        public DevEventsController(
            ILogger<DevEventsController> logger, 
            DevEventsDbContext context
            )
        {
            _logger = logger;
            _context = context;
            _logger.LogInformation("AwesomeDevEvents.API.DevEventsController");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventsController.GetAll()");

            var devEvents = _context
                .DevEvents.Where(d => !d.IsDeleted)
                .ToList();

            //var devEvents = await _context
            //  .DevEvents.Where(d => !d.IsDeleted)
            //  .ToListAsync();

            return Ok(devEvents);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventsController.GetById()");

            var devEvent = _context
                .DevEvents
                .SingleOrDefault(d => d.Id == id);

            //var devEvent = await _context
            //    .DevEvents
            //    .AsNoTracking()
            //    .Include(de => de.Speakers)
            //    .SingleOrDefaultAsync(d => d.Id == id);

            if (devEvent == null)
                return NotFound();

            return Ok(devEvent);
        }

        [HttpPost]
        public async Task<IActionResult> Post(DevEvent devEvent)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventsController.Post()");

            _context.DevEvents.Add(devEvent);

            //await _context.DevEvents.AddAsync(devEvent);
            //await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = devEvent.Id }, devEvent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, DevEvent input)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventsController.Update()");

            var devEvent = _context
                .DevEvents
                .SingleOrDefault(d => d.Id == id);

            //var devEvent = await _context
            //    .DevEvents
            //    .SingleOrDefaultAsync(d => d.Id == id);

            if (devEvent == null)
                return NotFound();

            devEvent.Update(
                title: input.Title, 
                description: input.Description, 
                startDate: input.StartDate, 
                endDate: input.EndDate
                );

            //_context.DevEvents.Update(devEvent);
            //await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventsController.Delete()");

            var devEvent = _context
                .DevEvents
                .SingleOrDefault(d => d.Id == id);

            //var devEvent = await _context
            //    .DevEvents
            //    .SingleOrDefaultAsync(d => d.Id == id);

            if (devEvent == null)
                return NotFound();

            devEvent.Delete();

            // await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("{id}/speakers")]
        public async Task<IActionResult> PostSpeaker(Guid id, DevEventSpeaker speaker)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventsController.PostSpeaker()");

            var devEvent = _context
               .DevEvents
               .SingleOrDefault(d => d.Id == id);

            if (devEvent == null)
                return NotFound();

            //var existDevEvent = _context
            //    .DevEvents
            //    .Any(d => d.Id == id);

            //var existDevEvent = await _context
            //    .DevEvents
            //    .AnyAsync(d => d.Id == id);

            //if (!existDevEvent)
            //    return NotFound();

            speaker.DevEventId = id;
            devEvent.Speakers.Add(speaker);

            // speaker.DevEventId = id;
            //await _context.DevEventSpeakers.AddAsync(speaker);
            //await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
