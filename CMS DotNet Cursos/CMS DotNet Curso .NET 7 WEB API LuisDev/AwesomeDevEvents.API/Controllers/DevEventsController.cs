using AwesomeDevEvents.Domain.Dtos;
using AwesomeDevEvents.Service;
using AwesomeDevEvents.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace AwesomeDevEvents.API.Controllers
{
    [Route("api/v1/dev-events")] // [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("fixed")]
    public class DevEventsController : ControllerBase
    {
        private readonly ILogger<DevEventsController> _logger;
        private IDevEventService _eventService;
        // private readonly ICacheService _cacheService;

        public DevEventsController(
            ILogger<DevEventsController> logger, 
            // ICacheService cacheService,
            IDevEventService eventService
            )
        {
            _logger = logger;
            _eventService = eventService ?? throw new ArgumentNullException(nameof(DevEventService));
            // _cacheService = cacheService;
            _logger.LogInformation("AwesomeDevEvents.API.DevEventsController");
        }


        [HttpGet("ping")]
        [EnableRateLimiting("sliding")]
        public IActionResult Ping() => Ok("pong");


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventsController.GetAll()");

            //var cacheData = _cacheService.GetData<IEnumerable<Product>>("product");
            //if (cacheData != null)
            //{
            //    return cacheData;
            //}
            //var expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
            // devEvents = await _eventService.GetAllAsync();
            //_cacheService.SetData<IEnumerable<Product>>("product", cacheData, expirationTime);

            var devEvents = await _eventService.GetAllAsync();
            return devEvents is null ? NotFound("No records found") : Ok(devEvents);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventsController.GetById()");

            //Product filteredData;
            //var cacheData = _cacheService.GetData<IEnumerable<Product>>("product");
            //if (cacheData != null)
            //{
            //    filteredData = cacheData.Where(x => x.ProductId == id).FirstOrDefault();
            //    return filteredData;
            //}
            //filteredData = _dbContext.Products.Where(x => x.ProductId == id).FirstOrDefault();

            var devEvent = await _eventService.GetByIdAsync(id);
            return devEvent?.id == Guid.Empty ? NotFound("No records found") : Ok(devEvent);
        }

        [HttpPost]
        public async Task<IActionResult> Post(DevEventInputDto input)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventsController.Post()");

            // _cacheService.RemoveData("product");

            var devEvent = await _eventService.InsertAsync(input);
            return devEvent?.id == Guid.Empty ? BadRequest() : CreatedAtAction(nameof(GetById), new { id = devEvent.id }, devEvent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, DevEventInputDto input)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventsController.Update()");

            //  _cacheService.RemoveData("product");

            var devEvent = await _eventService.UpdateAsync(id, input);
            return devEvent?.id == Guid.Empty ? BadRequest() : NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventsController.Delete()");

            //_cacheService.RemoveData("product");

            var status = await _eventService.DeleteAsync(id);
            return status ? NoContent() : BadRequest();
        }

        //[HttpPost("{id}/speakers")]
        //public async Task<IActionResult> PostSpeaker(Guid id, DevEventSpeakerInputDto input)
        //{
        //    //_logger.LogInformation("AwesomeDevEvents.API.DevEventsController.PostSpeaker()");

        //    //var isExistDevEvent = await _eventRepo.FindAnyAsync(id);
        //    //if (!isExistDevEvent)
        //    //    return NotFound();

        //    //input.DevEventId = id;

        //    //var devSpeaker = await _speakerRepo.CreateAsync(input);

        //    ////if (!devSpeaker.IsValid)
        //    ////    return BadRequest(devSpeaker.Notifications.ConvertToProblemDetails());

        //    //var resultCommit = await _uow.CommitAsync();

        //    //if (!resultCommit)
        //    //    return BadRequest();

        //    return NoContent();
        //}
    }
}
