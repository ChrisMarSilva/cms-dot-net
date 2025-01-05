using Microsoft.EntityFrameworkCore;
using RabbitMQ.Models.Entities;
using System.Reflection;

namespace RabbitMQ.Repositories.Database;

// dotnet tool update --global dotnet-ef
// dotnet build 

// dotnet ef migrations add InitialCreate
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

    public DbSet<Message> Messages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=postgres;User ID=postgres;Password=postgres;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(AppDbContext))!);

        //if (Database.IsNpgsql())
        //{
        //    var forceUtcDateTimeConverter = new ValueConverter<DateTime, DateTime>(
        //        to => DateTime.SpecifyKind(to, DateTimeKind.Utc),
        //        from => from);

        //    foreach (var property in modelBuilder.Model.GetEntityTypes()
        //                 .SelectMany(t => t.GetProperties())
        //                 .Where(p => p.ClrType == typeof(DateTime) ||
        //                             p.ClrType == typeof(DateTime?)))
        //    {
        //        property.SetValueConverter(forceUtcDateTimeConverter);
        //    }
        //}

        base.OnModelCreating(modelBuilder);
    }
}

