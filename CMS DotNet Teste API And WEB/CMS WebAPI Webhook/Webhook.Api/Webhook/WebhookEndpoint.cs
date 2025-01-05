namespace Webhook.Api.Webhook;

internal static class WebhookEndpoint
{
    public static void AddMapWebhookEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("webhook");

        group.MapPost("/subscription", (CreateWebhookRequestDto request, InMemoryWebhookRepository repository) =>
        {
            var subscription = new WebhookSubcription(Guid.NewGuid(), request.EventType, request.WebhookUrl, DateTime.UtcNow);

            repository.Add(subscription);

            return Results.Ok(subscription);
        })
        .WithTags("Webhook");

        group.MapGet("/subscription/{eventType}", (string eventType, InMemoryWebhookRepository repository) =>
        {
            var subscription = repository.GetByEventType(eventType);

            return subscription is null 
                ? Results.NotFound() 
                : Results.Ok(subscription);
        })
       .WithTags("Webhook");
    }
}