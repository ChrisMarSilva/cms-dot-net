using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection;

namespace Cache.Infra.Data.Context;

internal sealed class DataContext: DbContext
{
#if DEBUG
    //private static bool _scriptGenerated;
#endif

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
#if DEBUG
        //// ReSharper disable once InvertIf
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(DataContext))!);

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

        base.OnModelCreating(modelBuilder);
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
