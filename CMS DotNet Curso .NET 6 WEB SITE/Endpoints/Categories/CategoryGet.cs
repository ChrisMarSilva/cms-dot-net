namespace IWantApp.Endpoints.Categories;

public class CategoryGet
{
    public static string Template => "/categories/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action([FromRoute] Guid id, ApplicationDbContext context)
    {
        var category = await context.Categories.Where(c => c.Id == id).FirstOrDefaultAsync();

        if (category == null)
            return Results.NotFound("Category not found");

        var categoryResponse = new CategoryResponse(category.Id, category.Name, category.Active);

        return Results.Ok(categoryResponse);
    }

}
