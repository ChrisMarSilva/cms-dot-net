namespace IWantApp.Endpoints.Orders;

public class OrderGet
{
    public static string Template => "/orders/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize]
    public static async Task<IResult> Action([FromRoute] Guid id, HttpContext http, ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        var order = await context.Orders.AsNoTracking().Include(o => o.Products).FirstOrDefaultAsync(o => o.Id == id);

        if (order == null)
            return Results.NotFound("Order not found");

        var clientClaim = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier);
        var employeeClaim = http.User.Claims.FirstOrDefault(c => c.Type == "EmployeeCode").Value;

        if (order.ClientId != clientClaim.Value && employeeClaim == null)
            return Results.Forbid();

        var cliente = await userManager.FindByIdAsync(order.ClientId);
        var productsResponse = order.Products.Select(p => new OrderProduct(p.Id, p.Name));
        var orderResponse = new OrderResponse(order.Id, cliente.Email, productsResponse, order.Total, order.DeliveryAddress);

        return Results.Ok(orderResponse);
    }
}
