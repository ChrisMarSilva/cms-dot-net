namespace WebApi.Messaging.RabbitMQ.MassTransit.Domain.Contracts;

public record DriverNotificationRecord(
    Guid DriverId,
    string DriverName
);
