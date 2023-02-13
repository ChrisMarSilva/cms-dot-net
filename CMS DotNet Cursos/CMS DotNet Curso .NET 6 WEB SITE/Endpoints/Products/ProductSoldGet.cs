namespace IWantApp.Endpoints.Products;

public class ProductSoldGet
{
    public static string Template => "/products/sold"; // report
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action(QueryAllProductsSold query)
    {
        var result = await query.ExecuteAsync();

        return Results.Ok(result);
    }
}
