using Microsoft.EntityFrameworkCore;
using Project.Domain.Models;
using System.Data;
using System.Reflection;

namespace Project.Database.Context;

// dotnet tool install --global dotnet-ef
// dotnet tool update --global dotnet-ef
// dotnet build 

// dotnet ef migrations add AddTablesInit
// dotnet ef database update
// dotnet ef migrations remove

public class ApplicationDbContext : DbContext, IDataContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {  }

    public DbSet<ProductModel> Products { get; set; }     //public virtual DbSet<ProductModel> Products { get; set; } 
    public DbSet<ClientModel> Clients { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(ApplicationDbContext))!);

        base.OnModelCreating(builder);
    }

    public async Task OpenConnection()
    {
        if (Database.GetDbConnection().State != ConnectionState.Open)
        {
            try
            {
                await Database.OpenConnectionAsync();
            }
            catch
            {

            }
        }
    }
}

