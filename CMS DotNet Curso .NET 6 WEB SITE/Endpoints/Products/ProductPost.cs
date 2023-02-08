namespace IWantApp.Endpoints.Products;

public class ProductPost
{
    public static string Template => "/products";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action(
        ProductRequest productRequest,
        HttpContext http,
        ApplicationDbContext context)
    {
        var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == productRequest.CategoryId);
        //if (category == null)
        //    return Results.NotFound("Category not found");

        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

        var product = new Product(productRequest.Name, category, productRequest.Description, productRequest.IsStock, productRequest.Price, userId);
        
        if (!product.IsValid)
            return Results.ValidationProblem(product.Notifications.ConvertToProblemDetails());

        var productSaved = await context.Products.FirstOrDefaultAsync(c => c.Name == productRequest.Name);
        
        if (productSaved != null)
            return Results.BadRequest("Name exist");

        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();

        return Results.Created($"/{Template}/{product.Id}", product.Id);
    }
}
