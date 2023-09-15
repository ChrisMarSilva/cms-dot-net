using Microsoft.EntityFrameworkCore;
using System.Reflection;
using VShop.ProductApi.Models;

namespace VShop.ProductApi.Context;

public class AppDbContext : DbContext
{
    // https://github.com/dotnet-architecture/eShopOnContainers
    // https://github.com/macoratti/VShop1

    // dotnet tool install --global dotnet-ef
    // dotnet tool update --global dotnet-ef
    // dotnet build 

    // dotnet ef 
    // dotnet ef migrations add InicialTables
    // dotnet ef database update
    // dotnet ef migrations remove

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<CategoryModel> Categories { get; set; }
    public DbSet<ProductModel> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(AppDbContext))!);

        //mb.Entity<Category>().HasKey(c => c.CategoryId);
        //mb.Entity<Category>().Property(c => c.Name).HasMaxLength(100).IsRequired();

        //mb.Entity<Product>().Property(c => c.Name).HasMaxLength(100).IsRequired();
        //mb.Entity<Product>().Property(c => c.Description).HasMaxLength(255).IsRequired();
        //mb.Entity<Product>().Property(c => c.ImageURL).HasMaxLength(255).IsRequired();
        //mb.Entity<Product>().Property(c => c.Price).HasPrecision(12, 2);
        //mb.Entity<Category>().HasMany(g => g.Products).WithOne(c => c.Category).IsRequired().OnDelete(DeleteBehavior.Cascade);

        mb.Entity<CategoryModel>().HasData(
            new CategoryModel { CategoryId = 1, Name = "Material Escolar" },
            new CategoryModel { CategoryId = 2, Name = "Acessórios" }
        );
    }
}
