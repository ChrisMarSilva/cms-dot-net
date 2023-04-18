using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace Catalogo.API.Controllers.v1;

[ApiController]
[ApiVersion("1.0", Deprecated = true)]
[EnableQuery]
[Route("api/v{version:apiVersion}/teste")]
[Produces("application/json")]
[Consumes("application/json")]
[AllowAnonymous]
public class TesteV1Controller : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Content("<html><body><h2>TesteV1Controller - V 1.0 </h2></body></html>", "text/html");
    }
}
