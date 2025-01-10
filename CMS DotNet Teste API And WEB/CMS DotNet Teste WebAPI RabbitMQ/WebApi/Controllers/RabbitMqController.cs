using MassTransit;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Api.Extensions;
using RabbitMQ.Contratos.Requests;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RabbitMqController : ControllerBase
{
    private readonly ILogger<RabbitMqController> _logger;
    private readonly ISendEndpointProvider _sendEndpointProvider;
    private readonly IConfiguration _configuration;

    public RabbitMqController(ILogger<RabbitMqController> logger, ISendEndpointProvider sendEndpointProvider, IConfiguration configuration)
    {
        _logger = logger;
        _sendEndpointProvider = sendEndpointProvider;
        _configuration = configuration;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendMessage([FromBody] MensagemDto message, CancellationToken cancellationToken = default)
    {
        //if (message == null || string.IsNullOrWhiteSpace(message.Text))
        //    return BadRequest("A mensagem não pode ser vazia.");

        var queueName = _configuration["RabbitMQ:Queue"];
        //if (string.IsNullOrEmpty(queueName))
        //    return StatusCode(500, "Nome da fila não está configurado.");

        await _sendEndpointProvider.SendAsync(
            queue: queueName!,
            message: message,
            cancellationToken: cancellationToken); // CancellationToken.None

        return Ok($"Mensagem enviada com sucesso para a fila: {queueName}");
    }
}