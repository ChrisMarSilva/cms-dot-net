namespace Webhook.Api.Webhook;

internal sealed class WebhookDispatcher(HttpClient httpClient, InMemoryWebhookRepository repository)
{
   public async Task DispatchAsync(string eventType, object payload)
    {
        var subscriptions = repository.GetByEventType(eventType);

        foreach (var webhookSubscription in subscriptions)
        {
            var request = new
            {
                Id = Guid.NewGuid(),
                EventType = webhookSubscription.EventType,
                SubscriptionId = webhookSubscription.Id,
                Timestamp = DateTime.UtcNow,
                Data = payload
            };

            await httpClient.PostAsJsonAsync(webhookSubscription.WebhookUrl, request);
        }

        repository.Delete(eventType);
    }
}
