namespace IWantApp.Endpoints.Orders;

public class OrderPost
{
    public static string Template => "/orders";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "CpfPolicy")]
    public static async Task<IResult> Action(OrderRequest orderRequest, HttpContext http, ApplicationDbContext context)
    {
        var clientId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var clientName = http.User.Claims.First(c => c.Type == "Name").Value;

        //List<Product> productsFound = new List<Product>();
        //if (orderRequest.ProductIds != null && orderRequest.ProductIds.Any())
        //{
        //    foreach (var item in orderRequest.ProductIds)
        //    {
        //        var product = await context.Products.FirstOrDefaultAsync(p => p.Id == item);
        //        if (product != null)
        //            productsFound.Add(product);
        //    }
        //}

        List<Product> productsFound = null;
        if (orderRequest.ProductIds != null && orderRequest.ProductIds.Any())
            productsFound = await context.Products.Where(p => orderRequest.ProductIds.Contains(p.Id)).ToListAsync();

        var order = new Order(clientId, clientName, productsFound, orderRequest.DeliveryAddress);

        if (!order.IsValid)
            return Results.ValidationProblem(order.Notifications.ConvertToProblemDetails());

        await context.Orders.AddAsync(order);
        await context.SaveChangesAsync();

        return Results.Created($"/{Template}/{order.Id}", order.Id);
    }
}
