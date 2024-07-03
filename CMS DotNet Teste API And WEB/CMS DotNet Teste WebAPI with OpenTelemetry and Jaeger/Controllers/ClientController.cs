using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Project.Domain.Dtos.Request;
using Project.Domain.Dtos.Response;
using Project.Filters.Idempotency;
using Project.ServiceBus.Commands;
using System.Diagnostics;
using System.Net;

namespace Project.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
public class ClientController : ControllerBase
{
    private readonly ILogger<ClientController> _logger;
    private readonly IPublishEndpoint _publishEndpoin;

    public ClientController(ILogger<ClientController> logger, IPublishEndpoint publishEndpoin)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _publishEndpoin = publishEndpoin ?? throw new ArgumentNullException(nameof(publishEndpoin));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(ClientResponseDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponseDto))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseDto))]
    [ProducesDefaultResponseType(typeof(ErrorResponseDto))]
    [ValidateIdempotencyKey(headerName: "X-Idempotency-Key")]
    public async Task<ActionResult<ClientResponseDto>> Create([FromBody] ClientRequestDto request, [FromHeader(Name = "X-Idempotency-Key")] string idempotenceKey, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"ClientResponseDto.Create({request.Name}): {idempotenceKey}");

            //if (!Guid.TryParse(idempotenceKey, out _))
            //    return BadRequest();

            if (!request.IsValid())
                return BadRequest();

            var command = new ClientAddCommandDto(idempotenceKey, request);

            await _publishEndpoin.Publish(command, ctx =>
            {
                ctx.MessageId = command.CommandId; 
                ctx.Durable = true;
                ctx.Headers.Set("X-Idempotency-Key", idempotenceKey); 
            }, cancellationToken);

            cancellationToken.ThrowIfCancellationRequested();
            Activity.Current?.SetTag("command_id", command.CommandId);

            var response = new ClientResponseDto(idempotenceKey, command.CommandId, command.DtHrRequest);
            //return Ok(response);

            var idempotencyResult = Ok(response); // AcceptedAtAction("Consultar", new { numCtrlReq }, response);
            HttpContext.Items[IdempotencyOptions.IdempotencyResponseBodyKey] = idempotencyResult;

            return idempotencyResult;
        }
        catch (OperationCanceledException e)
        {
            _logger.LogError(e, "Operação cancelada pelo sistema cliente.");
            return StatusCode(StatusCodes.Status504GatewayTimeout);
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Falha crítica ao gerar a inclusão do terceiro autorizado. Segue a descrição: {description}", e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ErrorResponseDto.Begin(HttpStatusCode.InternalServerError, "Falha interna durante o processamento. Por favor, tente novamente."));
        }
    }
}
