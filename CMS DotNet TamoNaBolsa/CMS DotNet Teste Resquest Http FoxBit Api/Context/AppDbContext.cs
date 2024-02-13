using CMS_DotNet_Teste_Resquest_Http_FoxBit_Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CMS_DotNet_Teste_Resquest_Http_FoxBit_Api.Context;

// dotnet tool install --global dotnet-ef
// dotnet tool update --global dotnet-ef
// dotnet build 

// dotnet ef 
// dotnet ef migrations add AddTablesInitOnDataTablesDb - TradeModel
// dotnet ef migrations add AddTablesInitOnDataTablesDb02 - TradeModel
// dotnet ef migrations add AddTablesInitOnDataTablesDb03 - CurrencyModel
// dotnet ef migrations add AddTablesInitOnDataTablesDb04 - MarketModel
// dotnet ef migrations add AddTablesInitOnDataTablesDb05 - MarketQuoteModel
// dotnet ef migrations add AddTablesInitOnDataTablesDb06 - SystemTimeModel
// dotnet ef migrations add AddTablesInitOnDataTablesDb07 - MemberInfoModel
// dotnet ef migrations add AddTablesInitOnDataTablesDb08 - TradeModel

// dotnet ef database update
// dotnet ef migrations remove

public class AppDbContext : DbContext
{
    public AppDbContext() { }
    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) { }

    public DbSet<SystemTimeModel> SystemTimeModels { get; set; }
    public DbSet<CurrencyModel> Currencies { get; set; }
    public DbSet<MarketModel> Markets { get; set; }
    public DbSet<MarketQuoteModel> MarketQuotes { get; set; }
    public DbSet<MemberInfoModel> MemberInfos { get; set; }
    public DbSet<TradeModel> Trades { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(AppDbContext))!);
        base.OnModelCreating(builder);
    }
}
