namespace Webhook.Api.Webhook;

public sealed record WebhookSubcription(
    Guid Id,
    string EventType,
    string WebhookUrl,
    DateTime CreateUtc);

public sealed record CreateWebhookRequestDto(
    string EventType,
    string WebhookUrl);