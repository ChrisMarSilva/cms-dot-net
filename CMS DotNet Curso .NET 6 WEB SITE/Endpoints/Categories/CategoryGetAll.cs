﻿using IWantApp.Domain.Products;
using IWantApp.Infra.Data;

namespace IWantApp.Endpoints.Categories;

public class CategoryGetAll
{
    public static string Template => "/categories";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(ApplicationDbContext context)
    {
        var categories = context
            .Categories
            .ToList();

        if (categories == null || categories.Count <= 0)
            return Results.NotFound("Category not found");

        var categoryResponse = categories
            .Select(c => new CategoryResponse(c.Id, c.Name, c.Active ));

        return Results.Ok(categoryResponse);
    }

}