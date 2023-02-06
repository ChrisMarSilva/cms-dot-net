using IWantApp.Domain.Infra.Data;
using IWantApp.Domain.Products;

namespace IWantApp.Endpoints.Categories;

public class CategoryPost
{
    public static string Template => "/categories";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(CategoryRequest categoryRequest, ApplicationDbContext context)
    {
        var category = new Category(
            categoryRequest.Name, 
            "Chris MarSil Criado", 
            "Chris MarSil Editado"
        );

        if (!category.IsValid)
            return Results.ValidationProblem(
                category.Notifications.ConvertToProblemDetails()
            );

        var categorySaved = context
            .Categories
            .Where(c => c.Name == categoryRequest.Name)
            .FirstOrDefault();

        if (categorySaved != null)
            return Results.BadRequest("Name exist");

        context.Categories.Add(category);
        context.SaveChanges();

        return Results.Created($"/{Template}/{category.Id}", category.Id);
    }

}
