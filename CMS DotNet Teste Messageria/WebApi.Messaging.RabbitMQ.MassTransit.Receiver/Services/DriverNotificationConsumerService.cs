using MassTransit;
using WebApi.Messaging.RabbitMQ.MassTransit.Domain.Contracts;

namespace WebApi.Messaging.RabbitMQ.MassTransit.Receiver.Services;

public class DriverNotificationConsumerService : IConsumer<DriverNotificationRecord>
{
    private readonly ILogger<DriverNotificationConsumerService> _logger;

    public DriverNotificationConsumerService(
        ILogger<DriverNotificationConsumerService> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<DriverNotificationRecord> context)
    {
        // var serializedMessage = JsonSerializer.Serialize(context.Message, new JsonSerializerOptions { });

        _logger.LogInformation($"Consumer Log: {context.Message.DriverId} - {context.Message.DriverName}");

        return Task.CompletedTask;
    }
}
