namespace WebApi.Messaging.RabbitMQ.MassTransit.Sender.Services.Interfaces;

public interface IDriverNotificationPublisherService
{
    Task SendNotification(Guid driverId, string teamName);
}
