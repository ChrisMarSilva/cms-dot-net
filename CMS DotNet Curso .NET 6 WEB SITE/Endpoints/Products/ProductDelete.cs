namespace IWantApp.Endpoints.Products;

public class ProductDelete
{
    public static string Template => "/products/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Delete.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action([FromRoute] Guid id, ApplicationDbContext context)
    {
        //var product = await context.Products.Where(c => c.Id == id).FirstOrDefaultAsync();
        var product = await context.Products.FirstOrDefaultAsync(c => c.Id == id);

        if (product == null)
            return Results.NotFound("Product not found");

        context.Products.Remove(product);
        await context.SaveChangesAsync();

        return Results.Ok();
    }
}
