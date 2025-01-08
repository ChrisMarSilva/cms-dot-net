using Cache.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection;

namespace Cache.Api.Database.Contexts;

// dotnet tool update --global dotnet-ef
// dotnet build 

// dotnet ef migrations bundle --project Movies.Api/Movies.Api.csproj --output efbundle
// dotnet ef migrations add AddMovies
// dotnet ef database update
// dotnet ef migrations remove

public class AppDbContext : DbContext
{
    private static bool _scriptGenerated;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        //Database.SetInitializer<AppDbContext>(new CreateDatabaseIfNotExists<AppDbContext>());
        //Database.Initialize(false); // false = Somente ser exeutado uma vez dentro do DbContext]
        //Configuration.LazyLoadingEnabled = false;
        //Configuration.ProxyCreationEnabled = false;
        //Database.Log = comando => System.Diagnostics.Debug.WriteLine(comando);

        //if (!_scriptGenerated)
        //{
        //    _scriptGenerated = true;

        //    Console.WriteLine();
        //    Console.WriteLine();

        //    var color = Console.ForegroundColor;
        //    Console.ForegroundColor = ConsoleColor.Green;

        //    Console.WriteLine("-- =========================== INÍCIO DO SCRIPT 000-CrObj_PG.sql================================");
        //    Console.WriteLine(Database.GenerateCreateScript());
        //    Console.WriteLine("-- =========================== FINAL DO SCRIPT 000-CrObj_PG.sql================================");

        //    //try { Database.EnsureDeleted(); } catch (Exception e) { }
        //    // try { Console.ForegroundColor = ConsoleColor.Red; Database.EnsureCreated(); } catch (Exception e) { Console.WriteLine(e.Message); }

        //    Console.ForegroundColor = color;
        //    Console.WriteLine();
        //}
    }

    public DbSet<UserModel> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //if (!optionsBuilder.IsConfigured)
        //optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=postgres;User ID=postgres;Password=postgres;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(AppDbContext))!);

        base.OnModelCreating(modelBuilder);
    }

    public async Task OpenConnection()
    {
        if (Database.GetDbConnection().State != ConnectionState.Open)
            try { await Database.OpenConnectionAsync(); } catch { }
    }
}