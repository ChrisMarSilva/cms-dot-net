using CMS_DotNet_Teste_Resquest_Http_FoxBit.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CMS_DotNet_Teste_Resquest_Http_FoxBit.Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext() { }
    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) { }

    //public DbSet<CurrencyModel> Currencies { get; set; }
    //public DbSet<MarketModel> Markets { get; set; }
    //public DbSet<MarketQuoteModel> MarketQuotes { get; set; }
    //public DbSet<TradeModel> Trades { get; set; } //public virtual DbSet<TradeModel> Trades { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder opt)
    {
        if (!opt.IsConfigured)
        {
            var connectionString = "Server=localhost;Port=3306;Database=tamo_na_bolsa_foxbit;Uid=root;Pwd=Chrs8723;Persist Security Info=False;Connect Timeout=300;Connection Reset=False;Max Pool Size=300;";
            opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        base.OnConfiguring(opt);
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        //builder.Ignore<Notification>();
        builder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(AppDbContext))!);
    }
}
