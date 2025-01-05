
using CMS_DotNet_Teste_WebAPI_Database_Migrations.Movies;
using Microsoft.EntityFrameworkCore;

namespace CMS_DotNet_Teste_WebAPI_Database_Migrations.Database;

// dotnet tool update --global dotnet-ef
// dotnet build 

// dotnet ef migrations bundle --project Movies.Api/Movies.Api.csproj --output efbundle
// dotnet ef migrations add AddMovies
// dotnet ef database update
// dotnet ef migrations remove

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=postgres;User ID=postgres;Password=postgres;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>().HasIndex(x => x.YearOfRelease);
    }

    public DbSet<Movie> Movies { get; set; }
}
