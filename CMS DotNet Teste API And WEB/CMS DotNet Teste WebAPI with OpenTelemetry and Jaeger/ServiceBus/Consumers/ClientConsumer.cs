using MassTransit;
using Project.Service.Interfaces;
using Project.ServiceBus.Commands;

namespace Project.ServiceBus.Consumers;

internal sealed class ClientConsumer : IConsumer<ClientAddCommandDto>
{
    private readonly ILogger<ClientConsumer> _logger;
    private readonly IService _service;

    public ClientConsumer(ILogger<ClientConsumer> logger, IService service)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    public async Task Consume(ConsumeContext<ClientAddCommandDto> context)
    {
        _logger.LogInformation("ClientConsumer.Consume");
        await _service.ProcessarSolicAddClient(context.Message);

        //await context.Message.Publish("");
        //return Task.CompletedTask;
       // await context.RespondAsync(Task.CompletedTask);
    }
}
