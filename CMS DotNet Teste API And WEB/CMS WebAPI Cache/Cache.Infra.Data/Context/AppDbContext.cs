using Cache.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection;

namespace Cache.Infra.Data.Context;

// dotnet tool update --global dotnet-ef
// dotnet build 

// dotnet ef migrations bundle --project Movies.Api/Movies.Api.csproj --output efbundle
// dotnet ef migrations add AddMovies
// dotnet ef database update
// dotnet ef migrations remove
public sealed class AppDbContext : DbContext
{

#if DEBUG
    private static bool _scriptGenerated;
#endif

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        //Database.SetInitializer<AppDbContext>(new CreateDatabaseIfNotExists<AppDbContext>());
        //Database.Initialize(false); // false = Somente ser exeutado uma vez dentro do DbContext]
        //Configuration.LazyLoadingEnabled = false;
        //Configuration.ProxyCreationEnabled = false;
        //Database.Log = comando => System.Diagnostics.Debug.WriteLine(comando);

#if DEBUG
        //if (!_scriptGenerated)
        //{
        //    _scriptGenerated = true;

        //    Console.WriteLine();
        //    Console.WriteLine();

        //    var color = Console.ForegroundColor;
        //    Console.ForegroundColor = ConsoleColor.Green;

        //    Console.WriteLine("==================================================================");
        //    Console.WriteLine(Database.GenerateCreateScript());
        //    Console.WriteLine("==================================================================");

        //    //try { Database.EnsureDeleted(); } catch (Exception e) { }
        //    //try { Console.ForegroundColor = ConsoleColor.Red; Database.EnsureCreated(); } catch (Exception e) { Console.WriteLine(e.Message); }

        //    Console.ForegroundColor = color;
        //    Console.WriteLine();
        //}
#endif
    }

    public DbSet<UserModel> Users { get; set; }
    public DbSet<ProductModel> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //if (!optionsBuilder.IsConfigured)
        //optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=postgres;User ID=postgres;Password=postgres;");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(assembly: Assembly.GetAssembly(typeof(AppDbContext))!);

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
        //else if (Database.IsSqlServer())
        //{
        //    //Ajusta as colunas com "Collate" para o padrão MSSQL: SQL_Latin1_General_CP1_CS_AS
        //    foreach (var property in modelBuilder.Model.GetEntityTypes()
        //                 .SelectMany(t => t.GetProperties())
        //                 .Where(p => p.ClrType == typeof(string)))
        //    {
        //        var collation = property.GetCollation();

        //        if (collation?.Equals("C", StringComparison.Ordinal) ?? false)
        //        {
        //            property.SetCollation("SQL_Latin1_General_CP1_CS_AS");
        //        }
        //    }
        //}
        //else if (Database.IsOracle())
        //{
        //    //O oracle já é case sensitive, apenas remover
        //    foreach (var property in modelBuilder.Model.GetEntityTypes()
        //                 .SelectMany(t => t.GetProperties())
        //                 .Where(p => p.ClrType == typeof(string)))
        //    {
        //        var collation = property.GetCollation();

        //        if (collation?.Equals("C", StringComparison.Ordinal) ?? false)
        //        {
        //            property.SetCollation(null);
        //        }
        //    }
        //}

        base.OnModelCreating(builder);
    }

    public async Task OpenConnection()
    {
        if (Database.GetDbConnection().State != ConnectionState.Open)
        {
            try { await Database.OpenConnectionAsync(); } catch { }
        }
    }
}