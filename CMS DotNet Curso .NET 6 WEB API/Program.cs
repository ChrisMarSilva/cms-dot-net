using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
// builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration["Database:SqlServer"]); // builder.Configuration.GetConnectionString("Database:SqlServer") // Framework >= 6.0


var app = builder.Build();
var configuration = app.Configuration;
ProductRepository.Init(configuration);


app.MapGet("/", () => "Hello World!");

//if (app.Environment.IsStaging())
app.MapGet("/configuration/database", (IConfiguration configuration) =>
{
    return Results.Ok(configuration["Database:SqlServer"]);
});

app.MapPost("/products", (ProductRequest productRequest, ApplicationDbContext context) =>
{
    var category = context
        .Categories
        .Where(c => c.Id == productRequest.CategoryId)
        .FirstOrDefault();

    if (category == null)
        return Results.NotFound("Category Not Found");

    var product = new Product
    {
        Code = productRequest.Code,
        Name = productRequest.Name,
        Description = productRequest.Description,
        Category = category
    };

    if (productRequest.Tags != null)
    {
        product.Tags = new List<Tag>();
        foreach (var item in productRequest.Tags)
        {
            product.Tags.Add(new Tag { Name = item });
        }
    }

    // ProductRepository.Add(product);
    context.Products.Add(product);
    context.SaveChanges();

    return Results.Created($"/products/{product.Id}", product.Id);
});

app.MapGet("/products/{id}", ([FromRoute] int id, ApplicationDbContext context) =>
{
    // var product = ProductRepository.GetByCode(code);
    var product = context
        .Products
        .Include(p => p.Category)
        .Include(p => p.Tags)
        .Where(p => p.Id == id)
        .FirstOrDefault();

    if (product == null)
        return Results.NotFound("Product Not Found");

    return Results.Ok(product);
});

app.MapPut("/products/{id}", ([FromRoute] int id, ProductRequest productRequest, ApplicationDbContext context) =>
{
    //var product = ProductRepository.GetByCode(productRequest.Code);
    var product = context
        .Products
        .Include(p => p.Tags)
        .Where(p => p.Id == id)
        .FirstOrDefault();

    if (product == null)
        return Results.NotFound("Product Not Found");

    var category = context
        .Categories
        .Where(c => c.Id == productRequest.CategoryId)
        .FirstOrDefault();

    if (category == null)
        return Results.NotFound("Category Not Found");

    product.Code = productRequest.Code;
    product.Name = productRequest.Name;
    product.Description = productRequest.Description;
    product.Category = category;
    product.Tags = new List<Tag>();

    // if (product.Tags != null)
    // {
    //     product.Tags.Clear();
    //     // product.Tags = null;
    // }

    if (productRequest.Tags != null)
    {
        // product.Tags = new List<Tag>();
        foreach (var item in productRequest.Tags)
        {
            product.Tags.Add(new Tag { Name = item });
        }
    }

    context.SaveChanges();

    return Results.Ok();
});

app.MapDelete("/products/{id}", ([FromRoute] int id, ApplicationDbContext context) =>
{
    //var product = ProductRepository.GetByCode(code);
    var product = context
        .Products
        .Where(p => p.Id == id)
        .FirstOrDefault();

    if (product == null)
        return Results.NotFound("Product Not Found");

    // ProductRepository.Remove(product);

    // context.Products.Attach(product);
    context.Products.Remove(product);
    context.SaveChanges();

    return Results.Ok();
});

app.Run();

#region Comandos

//-----------------

// docker pull mcr.microsoft.com/mssql/server:2022-latest
// docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=@Sql2022" -p 1433:1433 --name sqlserver --hostname sql1 -d mcr.microsoft.com/mssql/server:2022-latest

// CREATE DATABASE TestDB;
// SELECT Name from sys.databases;
// USE TestDB;
// CREATE TABLE Inventory (id INT, name NVARCHAR(50), quantity INT);
// INSERT INTO Inventory VALUES (1, 'banana', 150); INSERT INTO Inventory VALUES (2, 'orange', 154);
// SELECT * FROM Inventory WHERE quantity > 152;

// dotnet build
// dotnet run
// dotnet watch run

// dotnet add package Microsoft.EntityFrameworkCore --version 7.0.2
// dotnet add package Microsoft.EntityFrameworkCore.Design --version 7.0.2
// dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 7.0.2
// dotnet tool install --global dotnet-outdated-tool
// dotnet outdated --upgrade

// Passo 01
// dotnet tool install --global dotnet-ef
// Passo 02
// dotnet ef migrations add CreateProduct
// dotnet build 
// dotnet ef migrations remove
// dotnet ef database update
// Passo 03
// dotnet ef migrations add AddDescriptionOnProduct
// dotnet ef migrations remove
// dotnet ef database update
// Passo 04
// dotnet ef database update 20230204164720_CreateProduct
// Passo 05
// dotnet ef migrations add ConfigureTableProduct
// dotnet ef database update
// Passo 06
// dotnet ef migrations add CreateCategory
// dotnet ef database update
// Passo 07
// dotnet ef migrations add CreateCategoryId
// dotnet ef database update
// Passo 08
// dotnet ef migrations add CreateTags
// dotnet ef database update
// Passo 09
// dotnet ef migrations add CreateTagId
// dotnet ef database update

// -----------------

#endregion
