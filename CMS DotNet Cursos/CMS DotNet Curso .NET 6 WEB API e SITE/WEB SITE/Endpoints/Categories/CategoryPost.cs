﻿namespace IWantApp.Endpoints.Categories;

public class CategoryPost
{
    public static string Template => "/categories";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action(
        CategoryRequest categoryRequest, 
        HttpContext http, 
        ApplicationDbContext context)
    {
        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var category = new Category(categoryRequest.Name, userId);

        if (!category.IsValid)
            return Results.ValidationProblem(category.Notifications.ConvertToProblemDetails());

        //var categorySaved = await context.Categories.Where(c => c.Name == categoryRequest.Name).FirstOrDefaultAsync();
        var categorySaved = await context.Categories.FirstOrDefaultAsync(c => c.Name == categoryRequest.Name);

        if (categorySaved != null)
            return Results.BadRequest("Name exist");

        await context.Categories.AddAsync(category);
        await context.SaveChangesAsync();

        return Results.Created($"/{Template}/{category.Id}", category.Id);
    }
}
