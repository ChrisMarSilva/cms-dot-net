using IWantApp.Domain.Products;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace IWantApp.Endpoints.Categories;

public class CategoryGetAll
{
    public static string Template => "/categories";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action(int? page, int? rows, ApplicationDbContext context)
    {
        page = page == null || page <= 0 ? 1 : page;
        rows = rows == null || rows <= 0 ? 10 : rows;

        var categories = await context.Categories.AsNoTracking().OrderBy(p => p.Name).Skip((page.Value - 1) * rows.Value).Take(rows.Value).ToListAsync();

        //if (categories == null || categories.Count <= 0)
        //    return Results.NotFound("Category not found");

        var results = categories.Select(c => new CategoryResponse(c.Id, c.Name, c.Active ));

        return Results.Ok(results);
    }
}
