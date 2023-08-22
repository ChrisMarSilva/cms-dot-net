using Microsoft.AspNetCore.Mvc;

namespace Catalogo.API.Controllers.v2;

[Produces("application/json")]
[ApiVersion("2.0")]
//[Route("api/teste")]
//[Route("api/v{v:apiVersion}/teste")]
[Route("api/v{version:apiVersion}/teste")]
[ApiController]
public class TesteV2Controller : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Content("<html><body><h2>TesteV2Controller - V 2.0 </h2></body></html>", "text/html");
    }
}
