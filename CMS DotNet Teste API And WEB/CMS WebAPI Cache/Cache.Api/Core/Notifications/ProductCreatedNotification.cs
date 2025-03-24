using MediatR;

namespace Cache.Api.Core.Notifications;

public record ProductCreatedNotification(Guid Id) : INotification;