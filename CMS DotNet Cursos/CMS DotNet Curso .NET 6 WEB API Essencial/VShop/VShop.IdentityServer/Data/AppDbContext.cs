using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace VShop.IdentityServer.Data;

//.NET 6 SDK: dotnet new --install Duende.IdentityServer.Templates
//.NET 7 SDK: dotnet new install Duende.IdentityServer.Templates
// dotnet new isui

// dotnet tool install --global dotnet-ef
// dotnet tool update --global dotnet-ef
// dotnet build 

// dotnet ef 
// dotnet ef migrations add InicialTables
// dotnet ef database update
// dotnet ef migrations remove

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
