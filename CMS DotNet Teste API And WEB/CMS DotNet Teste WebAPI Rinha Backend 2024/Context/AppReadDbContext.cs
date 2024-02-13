using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using Rinha.Backend._2024.API.Context.Interfaces;
using Rinha.Backend._2024.API.Models.Domains.Read;
using System.Data;

namespace Rinha.Backend._2024.API.Context;

internal sealed class AppReadDbContext : DbContext, IDataContext
{
    public AppReadDbContext(DbContextOptions<AppReadDbContext> options)
        : base(options) { }

    public DbSet<ClienteReadModel> Clientes { get; set; }
    public DbSet<ClienteCarteiraReadModel> Carteiras { get; set; }
    public DbSet<ClienteTransacaoReadModel> Transacoes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Ignore<Notification>();
        //builder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(AppDbContext))!);
        builder.ApplyConfigurationsFromAssembly(typeof(AppReadDbContext).Assembly, ReadConfigurationsFilter);
        base.OnModelCreating(builder);
    }

    private static bool ReadConfigurationsFilter(Type type) => type.FullName?.Contains("Configurations.Read") ?? false;

    public async Task OpenConnection()
    {
        if (Database.GetDbConnection().State != ConnectionState.Open)
            try { await Database.OpenConnectionAsync(); } catch { }
    }
}
