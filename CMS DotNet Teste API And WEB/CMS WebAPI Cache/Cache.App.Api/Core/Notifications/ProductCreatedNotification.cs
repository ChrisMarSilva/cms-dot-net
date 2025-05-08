using MediatR;

namespace Cache.Shared.Core.Notifications;

public record ProductCreatedNotification(Guid Id) : INotification;