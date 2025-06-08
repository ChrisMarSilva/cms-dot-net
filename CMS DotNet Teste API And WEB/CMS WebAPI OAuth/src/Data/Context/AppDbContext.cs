using CMS_WebAPI_OAuth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CMS_WebAPI_OAuth.Data.Context;

// dotnet tool update --global dotnet-ef
// dotnet build 

// dotnet ef migrations add InitialTable
// dotnet ef migrations add UpdateTable
// dotnet ef migrations add SeedTable
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
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(assembly: Assembly.GetAssembly(typeof(AppDbContext))!);

        builder.Entity<ApplicationUser>().ToTable("TbAuth_Usuario");
        builder.Entity<IdentityUserClaim<Guid>>().ToTable("TbAuth_UsuarioReiv");
        builder.Entity<IdentityUserLogin<Guid>>().ToTable("TbAuth_UsuarioLogin");
        builder.Entity<IdentityUserToken<Guid>>().ToTable("TbAuth_UsuarioToken");
        builder.Entity<ApplicationRole>().ToTable("TbAuth_Funcao"); // ApplicationRole
        builder.Entity<IdentityRoleClaim<Guid>>().ToTable("TbAuth_FuncaoReiv");
        builder.Entity<IdentityUserRole<Guid>>().ToTable("TbAuth_UsuarioFuncao");

        //var idRoleAuthApi = Guid.NewGuid();

        //builder.Entity<ApplicationRole>().HasData(new ApplicationRole
        //{
        //    Id = idRoleAuthApi,
        //    Name = "OAuth - API",
        //    NormalizedName = "OAUTH_API",
        //    ConcurrencyStamp = Guid.NewGuid().ToString()
        //});

        //var usuarioAdmin = new ApplicationUser//("Administrador")
        //{
        //    Id = Guid.NewGuid(),
        //    FirstName = "Administrador",
        //    LastName = null,
        //    UserName = "admin",
        //    NormalizedUserName = "ADMIN",
        //    Email = "admin@example.com",
        //    NormalizedEmail = "ADMIN@EXAMPLE.COM",
        //    EmailConfirmed = true,
        //    PasswordHash = @"AQAAAAEAACcQAAAAEIhFJkyWZsWH6MChEVuFCzF/S9NtwVv77AC0Ldlh6UBN82/m+WKLSaU8oyLnSQVtow==",
        //    SecurityStamp = "JGTOKMN3V2JEFB55CMK3ANJEE527U5VG",
        //    ConcurrencyStamp = "14bef64f-be72-4b4c-a948-bb2e2b965945"
        //};
        //builder.Entity<ApplicationUser>().HasData(usuarioAdmin);

        //builder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
        //{
        //    RoleId = idRoleAuthApi,
        //    UserId = usuarioAdmin.Id
        //});

        //builder.Entity<IdentityUserClaim<Guid>>().HasData(new IdentityUserClaim<Guid>
        //{
        //    Id = -1,
        //    ClaimType = ClaimTypes.Role,
        //    ClaimValue = "admin",
        //    UserId = usuarioAdmin.Id
        //});
    }
}
