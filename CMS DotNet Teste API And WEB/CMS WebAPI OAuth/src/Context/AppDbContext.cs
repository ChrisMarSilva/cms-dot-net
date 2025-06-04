using CMS_WebAPI_OAuth.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CMS_WebAPI_OAuth.Context;

// dotnet tool update --global dotnet-ef
// dotnet build 

// dotnet ef migrations bundle --project Movies.Api/Movies.Api.csproj --output efbundle
// dotnet ef migrations add AddMovies
// dotnet ef database update
// dotnet ef migrations remove

public sealed class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{

#if DEBUG
    private static bool _scriptGenerated;
#endif

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
#if DEBUG
        if (!_scriptGenerated)
        {
            _scriptGenerated = true;

            Console.WriteLine();
            Console.WriteLine();

            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("==================================================================");
            Console.WriteLine(Database.GenerateCreateScript());
            Console.WriteLine("==================================================================");

            //try { Database.EnsureDeleted(); } catch (Exception e) { }
            //try { Console.ForegroundColor = ConsoleColor.Red; Database.EnsureCreated(); } catch (Exception e) { Console.WriteLine(e.Message); }

            Console.ForegroundColor = color;
            Console.WriteLine();
        }
#endif
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {

        //builder.Entity<IdentityUserClaim<Guid>>().ToTable("TbAuth_UsuarioReiv");
        //builder.Entity<IdentityUserLogin<Guid>>().ToTable("TbAuth_UsuarioLogin");
        //builder.Entity<IdentityUserToken<Guid>>().ToTable("TbAuth_UsuarioToken");
        //builder.Entity<IdentityRoleClaim<Guid>>().ToTable("TbAuth_FuncaoReiv");
        //builder.Entity<IdentityUserRole<Guid>>().ToTable("TbAuth_UsuarioFuncao");

        base.OnModelCreating(builder);
    }
}
