namespace Webhook.Api.Order;

public class Order
{
    public Order()
    {
        Id = Guid.NewGuid();
        Created = DateTime.UtcNow;
    }

    public Order(string customerName, decimal amount) : this()
    {
        CustomerName = customerName;
        Amount = amount;
    }

    public Guid Id { get; init; }
    public string CustomerName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime Created { get; set; }
}

public sealed record CreateOrderRequestDto(
    string CustomerName,
    decimal Amount);