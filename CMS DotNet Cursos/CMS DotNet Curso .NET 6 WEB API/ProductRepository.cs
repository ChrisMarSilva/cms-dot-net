
#region Repositories

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

#endregion 


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
