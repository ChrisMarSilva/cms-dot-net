using Microsoft.AspNetCore.Mvc;

namespace Catalogo.API.Controllers.v1;

[Produces("application/json")]
[ApiVersion("1.0", Deprecated = true)]
//[Route("api/v{v:apiVersion}/teste")]
[Route("api/v{version:apiVersion}/teste")]
[ApiController]
public class TesteV1Controller : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Content("<html><body><h2>TesteV1Controller - V 1.0 </h2></body></html>", "text/html");
    }
}
