namespace IWantApp.Endpoints.Categories;

public class CategoryDelete
{
    public static string Template => "/categories/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Delete.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action([FromRoute] Guid id, ApplicationDbContext context)
    {
        // var category = await context.Categories.Where(c => c.Id == id).FirstOrDefaultAsync();
        var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == id);

        if (category == null)
            return Results.NotFound("Category not found");

        context.Categories.Remove(category);
        await context.SaveChangesAsync();

        return Results.Ok();
    }
}
