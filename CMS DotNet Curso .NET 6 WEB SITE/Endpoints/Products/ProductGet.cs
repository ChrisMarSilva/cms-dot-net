namespace IWantApp.Endpoints.Products;

public class ProductGet
{
    public static string Template => "/products/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action([FromRoute] Guid id, ApplicationDbContext context)
    {
        //var product = await context.Products.Include(p => p.Category).Where(c => c.Id == id).FirstOrDefaultAsync();
        var product = await context.Products.AsNoTracking().Include(p => p.Category).FirstOrDefaultAsync(c => c.Id == id);

        if (product == null)
            return Results.NotFound("Product not found");

        var result = new ProductResponse(product.Id, product.Name, product.Category.Name, product.Description, product.IsStock, product.Price, product.Active);

        return Results.Ok(result);
    }
}
