using MassTransit;
using WebApi.Messaging.RabbitMQ.MassTransit.Domain.Contracts;
using WebApi.Messaging.RabbitMQ.MassTransit.Sender.Services.Interfaces;

namespace WebApi.Messaging.RabbitMQ.MassTransit.Sender.Services;

public class DriverNotificationPublisherService : IDriverNotificationPublisherService
{
    private readonly ILogger<DriverNotificationPublisherService> _logger;
    //private readonly IBus _bus;
    private readonly IPublishEndpoint _publishEndpoint;

    public DriverNotificationPublisherService(
        ILogger<DriverNotificationPublisherService> logger,
        //IBus bus,
        IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        //_bus = bus;
        _publishEndpoint = publishEndpoint;
    }

    public async Task SendNotification(Guid driverId, string teamName)
    {
        _logger.LogInformation($"Driver Notification for {driverId} - {teamName}");

        // await _bus.Publish(new DriverNotificationRecord(driverId, teamName));
        await _publishEndpoint.Publish(new DriverNotificationRecord(driverId, teamName));
    }
}
