using Microsoft.AspNetCore.Mvc;
using WebApi.Messaging.RabbitMQ.MassTransit.Sender.Services.Interfaces;

namespace WebApi.Messaging.RabbitMQ.MassTransit.Sender.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DriversController : ControllerBase
{
    private readonly ILogger<DriversController> _logger;
    private readonly IDriverNotificationPublisherService _driverNotificationService;

    public DriversController(
        ILogger<DriversController> logger,
        IDriverNotificationPublisherService driverNotificationService)
    {
        _logger = logger;
        _driverNotificationService = driverNotificationService;
    }

    [HttpGet]
    [Route("{driverId:Guid}")]
    public async Task<IActionResult> GetDriver(Guid driverId)
    {
        //var driver = await _unitOfWork.Driver.GetById(driverId);

        //if (driver == null) 
        //    return NotFound();

        //var result _mapper.Map<GetDriverResponder>(driver);

        //return Ok(result);

        await Task.Delay(1);

        return Ok();
    }

    [HttpPost("")]
    public async Task<IActionResult> AddDriver() // [FromBody] CreateDriverRequest driver
    {
        //if(!ModelState.IsValid)
        //    return BadRequest();

        //var result _mapper.Map<Driver>(driver);

        //await _unitOfWork.Driver.Add(result);
        //await _unitOfWork.CompleteAsync();

        //await _driverNotificationService.SendNotification(result.Id, result.Name);

        //return CreatedAtAction(nameof(GetDriver), new { driverId  = result.Id });

        var driverId = Guid.NewGuid();
        await _driverNotificationService.SendNotification(driverId, $"Teste {driverId}");

        return Ok();  // return CreatedAtAction(nameof(GetDriver), new { driverId = driverId });
    }
}
