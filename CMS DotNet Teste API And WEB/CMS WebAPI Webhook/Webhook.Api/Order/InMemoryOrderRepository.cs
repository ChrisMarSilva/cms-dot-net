namespace Webhook.Api.Order;

internal sealed class InMemoryOrderRepository
{
    private readonly List<Order> _repository = [];

    public void Add(Order order) =>
        _repository.Add(order);

    public IReadOnlyList<Order> GetAll() =>
        _repository.AsReadOnly();
}
