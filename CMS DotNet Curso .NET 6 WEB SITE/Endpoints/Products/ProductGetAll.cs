using System.Linq;

namespace IWantApp.Endpoints.Products;

public class ProductGetAll
{
    public static string Template => "/products";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action(int? page, int? rows, ApplicationDbContext context)
    {
        page = page == null || page <= 0 ? 1 : page;
        rows = rows == null || rows <= 0 ? 10 : rows;

        var products = await context.Products.AsNoTracking().Include(p => p.Category).OrderBy(p => p.Name).Skip((page.Value - 1) * rows.Value).Take(rows.Value).ToListAsync();

        //if (products == null || products.Count <= 0)
        //    return Results.NotFound("Product not found");

        var results = products.Select(p => new ProductResponse(p.Id, p.Name, p.Category.Name, p.Description, p.IsStock, p.Price, p.Active));

        return Results.Ok(results);
    }
}
