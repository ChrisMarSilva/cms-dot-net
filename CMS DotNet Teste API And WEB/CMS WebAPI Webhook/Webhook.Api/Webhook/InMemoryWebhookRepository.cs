namespace Webhook.Api.Webhook;

internal sealed class InMemoryWebhookRepository
{
    private readonly List<WebhookSubcription> _repository = [];

    public void Add(WebhookSubcription webhook) =>
        _repository.Add(webhook);

    public IReadOnlyList<WebhookSubcription> GetByEventType(string eventType) =>
        _repository.Where(x => x.EventType == eventType).ToList().AsReadOnly();

    public void Delete(Guid id) =>
        _repository.RemoveAll(x => x.Id == id);

    public void Delete(string eventType) =>
        _repository.RemoveAll(x => x.EventType == eventType);
}
