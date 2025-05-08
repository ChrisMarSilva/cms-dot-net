using MediatR;
using Microsoft.Extensions.Logging;

namespace Cache.Shared.Core.Notifications;

public class StockAssignedHandler(ILogger<StockAssignedHandler> logger) : INotificationHandler<ProductCreatedNotification>
{
    public Task Handle(ProductCreatedNotification notification, CancellationToken cancellationToken)
    {
        logger.LogInformation($"handling notification for product creation with id : {notification.Id}. assigning stocks.");

        return Task.CompletedTask;
    }
}