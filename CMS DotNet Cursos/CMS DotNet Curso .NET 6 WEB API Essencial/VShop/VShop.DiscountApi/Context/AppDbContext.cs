using Microsoft.EntityFrameworkCore;

namespace VShop.DiscountApi.Context;

// dotnet tool install --global dotnet-ef
// dotnet tool update --global dotnet-ef
// dotnet build 

// dotnet ef 
// dotnet ef migrations add InicialTables
// dotnet ef database update
// dotnet ef migrations remove

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    //Fluent API
    protected override void OnModelCreating(ModelBuilder mb)
    {
        
    }
}
