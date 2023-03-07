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

        // https://github.com/nishanc/PerformanceAndBestPractices/blob/main/PerformanceDemos/RedisOnDotnet6Demo/RedisOnDotnet6Demo/Controllers/HomeController.cs
        //public async Task<IActionResult> Index()
        //{
        //    List<User>? users;
        //    string loadLocation;
        //    string isCacheData;
        //    string recordKey = $"Users_{DateTime.Now:yyyyMMdd_hhmm}";

        //    users = await _cache.GetRecordAsync<List<User>>(recordKey);

        //    if (users is null) // Data not available in the Cache
        //    {
        //        users = await _userRepository.GetUsersAsync();
        //        loadLocation = $"Loaded from DB at {DateTime.Now}";
        //        isCacheData = "text-danger";

        //        await _cache.SetRecordAsync(recordKey, users);
        //    }
        //    else // Data available in the Cache
        //    {
        //        loadLocation = $"Loaded from Cache at {DateTime.Now}";
        //        isCacheData = "text-success";
        //    }

        //    ViewData["Style"] = isCacheData;
        //    ViewData["Location"] = loadLocation;

        //    return View(users);
        //}



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
            try
            {
                // _cacheService.RemoveData("product");
                var devEvent = await _eventService.InsertAsync(input);
                return devEvent?.id == Guid.Empty ? BadRequest() : CreatedAtAction(nameof(GetById), new { id = devEvent.id }, devEvent);
            }
            catch (System.Exception ex)
            {
                // Console.Write(ex.Message);
                return StatusCode(500);
            }
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
