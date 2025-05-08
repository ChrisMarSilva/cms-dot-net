using Cache.Domain.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection;

namespace Cache.Infra.Data.Context;

public sealed class AuthContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{

#if DEBUG
    private static bool _scriptGenerated;
#endif

    public AuthContext(DbContextOptions options) : base(options)
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //if (!optionsBuilder.IsConfigured)
        //optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=postgres;User ID=postgres;Password=postgres;");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(assembly: Assembly.GetAssembly(typeof(AuthContext))!);

        builder.Entity<IdentityUserClaim<Guid>>().ToTable("TbAuth_UsuarioReiv");
        builder.Entity<IdentityUserLogin<Guid>>().ToTable("TbAuth_UsuarioLogin");
        builder.Entity<IdentityUserToken<Guid>>().ToTable("TbAuth_UsuarioToken");
        builder.Entity<IdentityRoleClaim<Guid>>().ToTable("TbAuth_FuncaoReiv");
        builder.Entity<IdentityUserRole<Guid>>().ToTable("TbAuth_UsuarioFuncao");

        //var converter = new ValueConverter<int, decimal>(
        //    v => v,
        //    v => (int)v,
        //    new ConverterMappingHints(valueGeneratorFactory: (p, t) => new TemporaryIntValueGenerator()));

        //builder.Entity<IdentityUserClaim<Guid>>().Property(x => x.Id)
        //    .ValueGeneratedOnAdd()
        //    .HasColumnType("numeric(10)")
        //    .HasConversion(converter);

        //foreach (var property in builder.Model.GetEntityTypes()
        //                                      .SelectMany(t => t.GetProperties())
        //                                      .Where(p => p.ClrType == typeof(string)))
        //{
        //    property.SetIsUnicode(false);
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