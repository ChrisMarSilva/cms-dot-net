using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var configuration = app.Configuration;
ProductRepository.Init(configuration);


app.MapGet("/", () => "Hello World!");

app.MapPost("/products", (Product product) =>
{
    ProductRepository.Add(product);

    return Results.Created($"/products/{product.Code}", product.Code);
});

app.MapGet("/products/{code}", ([FromRoute] string code) =>
{
    var productSaved = ProductRepository.GetByCode(code);
    if (productSaved == null)
        return Results.NotFound();

    return Results.Ok(productSaved);
});

app.MapPut("/products", (Product product) =>
{
    var productSaved = ProductRepository.GetByCode(product.Code);
    if (productSaved == null)
        return Results.NotFound();

    productSaved.Code = product.Code;
    productSaved.Name = product.Name;

    return Results.Ok();
});

app.MapDelete("/products/{code}", ([FromRoute] string code) =>
{
    var productSaved = ProductRepository.GetByCode(code);
    if (productSaved == null)
        return Results.NotFound();

    ProductRepository.Remove(productSaved);

    return Results.Ok();
});

if (app.Environment.IsStaging())
    app.MapGet("/configuration/database", (IConfiguration configuration) =>
    {
        return Results.Ok(configuration["database:connection"] + "/" + configuration["database:port"]);
    });


app.Run();

public static class ProductRepository
{
    private static List<Product> Products { get; set; } = Products = new List<Product>();

    public static void Init(IConfiguration configuration)
    {
        var products = configuration.GetSection("Products").Get<List<Product>>();
        Products = products;
    }

    public static void Add(Product product)
    {
        // if (Products == null)
        //     Products = new List<Product>();
        Products.Add(product);
    }

    public static Product GetByCode(string code)
    {
        return Products.FirstOrDefault(p => p.Code == code);
    }

    public static void Remove(Product product)
    {
        Products.Remove(product);
    }

}

public class Product
{
    public string Code { get; set; }
    public string Name { get; set; }
}

// dotnet run
// dotnet watch run