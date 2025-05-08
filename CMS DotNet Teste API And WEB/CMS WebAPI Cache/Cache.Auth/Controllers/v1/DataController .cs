using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cache.Auth.Controllers.v1;

[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
[Authorize]
public class DataController : ControllerBase
{
    private readonly ILogger<DataController> _logger;

    public DataController(ILogger<DataController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetData() =>
        Ok(new { message = "Protected data retrieved successfully!" });
}