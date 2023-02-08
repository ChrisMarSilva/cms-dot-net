namespace IWantApp.Endpoints.Products;

public class ProductPut
{
    public static string Template => "/products/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action(
        [FromRoute] Guid id,
        ProductRequest productRequest,
        HttpContext http,
        ApplicationDbContext context)
    {
        var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == productRequest.CategoryId);

        //if (category == null)
        //    return Results.NotFound("Category not found");

        //var product = await context.Products.Include(p => p.Category).Where(c => c.Id == id).FirstOrDefaultAsync();
        var product = await context.Products.Include(p => p.Category).FirstOrDefaultAsync(c => c.Id == id);

        if (product == null)
            return Results.NotFound("Product not found");

        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

        product.EditInfo(productRequest.Name, category, productRequest.Description, productRequest.IsStock, productRequest.Price, productRequest.Active, userId);

        if (!product.IsValid)
            return Results.ValidationProblem(product.Notifications.ConvertToProblemDetails());

        await context.SaveChangesAsync();

        return Results.Ok();
    }
}
