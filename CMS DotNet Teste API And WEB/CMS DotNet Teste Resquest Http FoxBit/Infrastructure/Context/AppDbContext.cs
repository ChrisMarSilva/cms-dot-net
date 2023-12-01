using CMS_DotNet_Teste_Resquest_Http_FoxBit.Domain.Models;
using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace CMS_DotNet_Teste_Resquest_Http_FoxBit.Infrastructure.Context;

// dotnet tool install --global dotnet-ef
// dotnet tool update --global dotnet-ef
// dotnet build 

// dotnet ef 
// dotnet ef migrations add AddTablesInitOnDataTablesDb
// dotnet ef database update
// dotnet ef migrations remove

public class AppDbContext : DbContext
{
    public AppDbContext()  { }
    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) { }

    public DbSet<TradeModel> Trades { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder opt)
    {
        //opt.EnableSensitiveDataLogging();
        //opt.EnableServiceProviderCaching();
        //opt.LogTo(Console.WriteLine, LogLevel.Information);
        if (!opt.IsConfigured)
        {
            var connectionString = "Server=localhost;Port=3306;Database=tamo_na_bolsa_foxbit;Uid=root;Pwd=Chrs8723;Persist Security Info=False;Connect Timeout=300;Connection Reset=False;Max Pool Size=300;";
            opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            // opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
        base.OnConfiguring(opt);
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Ignore<Notification>();

        builder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(AppDbContext))!);

        base.OnModelCreating(builder);
    }

}
