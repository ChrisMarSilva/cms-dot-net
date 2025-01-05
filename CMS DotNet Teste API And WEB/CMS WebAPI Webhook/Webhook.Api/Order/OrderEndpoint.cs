using Webhook.Api.Webhook;

namespace Webhook.Api.Order;

internal static class OrderEndpoint
{
    public static void AddMapOrderEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("orders");

        group.MapPost("/", async (CreateOrderRequestDto request, InMemoryOrderRepository repository, WebhookDispatcher webhookDispatcher) =>
        {
            var order = new Order(request.CustomerName, request.Amount);

            repository.Add(order);

            await webhookDispatcher.DispatchAsync("order.created", order);

            return Results.Ok(order);
        })
        .WithTags("Orders");

        group.MapGet("/", (InMemoryOrderRepository repository) =>
        {
            return Results.Ok(repository.GetAll());
        })
       .WithTags("Orders");
    }
}