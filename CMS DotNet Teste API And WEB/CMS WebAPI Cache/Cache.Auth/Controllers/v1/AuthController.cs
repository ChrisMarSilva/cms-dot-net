using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Cache.Auth.Controllers.v1;

[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;

    public AuthController(ILogger<AuthController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetData(CancellationToken cancellationToken = default)
    {
        try
        {
            var response = new { message = "Solicitação realizada com sucesso!" };

            return Ok(response);
        }
        catch (OperationCanceledException e)
        {
            _logger.LogError(e, "Operação cancelada pelo sistema cliente.");
            return StatusCode(StatusCodes.Status504GatewayTimeout);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Falha geral, segue a descrição: {description}.", ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, "Falha interna durante o processamente. Favor tentar novamente");
        }
    }

}
