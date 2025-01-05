using Microsoft.EntityFrameworkCore;

namespace Webhook.Api.Database;

// dotnet tool update --global dotnet-ef
// dotnet build 

// dotnet ef migrations bundle --project Movies.Api/Movies.Api.csproj --output efbundle
// dotnet ef migrations add AddMovies
// dotnet ef database update
// dotnet ef migrations remove

public class AppDbContext : DbContext
{
    //private static bool _scriptGenerated;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
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

    public DbSet<Order.Order> Orders { get; set; }
    public DbSet<Webhook.WebhookSubcription> WebhookSubcriptions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=postgres;User ID=postgres;Password=postgres;");
    }
}
