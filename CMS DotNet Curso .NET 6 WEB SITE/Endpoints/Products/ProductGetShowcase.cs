namespace IWantApp.Endpoints.Products;

public class ProductGetShowcase
{
    public static string Template => "/products/showcase";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [AllowAnonymous]
    public static async Task<IResult> Action(ApplicationDbContext context, int page = 1, int rows = 10, string orderBy = "name")
    {
        // page = page == null || page <= 0 ? 1 : page;
        // rows = rows == null || rows <= 0 || rows >= 10 ? 10 : rows;
        // orderBy = string.IsNullOrEmpty(orderBy) ? "name" : orderBy;

        if (rows > 10) 
            return Results.Problem(title: "Row with max 10", statusCode: 400);

        if (orderBy != "name" && orderBy != "price")
            return Results.Problem(title: "Order only by price or name", statusCode: 400);

        // queryBase ou queryFilter 
        var queryBase = context.Products.AsNoTracking().Include(p => p.Category).Where(p => p.IsStock && p.Category.Active);
       
        if (orderBy == "name")
            queryBase = queryBase.OrderBy(p => p.Name);
        else if (orderBy == "price")
            queryBase = queryBase.OrderBy(p => p.Price);

        var products = await queryBase.Skip((page - 1) * rows).Take(rows).ToListAsync();

        if (products == null || products.Count <= 0)
            return Results.NotFound("Product not found");

        var results = products.Select(p => new ProductResponse(p.Id, p.Name, p.Category.Name, p.Description, p.IsStock, p.Price, p.Active));

        return Results.Ok(results);
    }
}
