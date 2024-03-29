﻿namespace IWantApp.Endpoints.Categories;

public class CategoryPut
{
    public static string Template => "/categories/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action(
        [FromRoute] Guid id,
        CategoryRequest categoryRequest,
        HttpContext http,
        ApplicationDbContext context)
    {
        //var category = await context.Categories.Where(c => c.Id == id).FirstOrDefaultAsync();
        var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == id);

        if (category == null)
            return Results.NotFound("Category not found");

        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

        category.EditInfo(categoryRequest.Name, categoryRequest.Active, userId);

        if (!category.IsValid)
            return Results.ValidationProblem(category.Notifications.ConvertToProblemDetails());

        await context.SaveChangesAsync();

        return Results.Ok();
    }
}
