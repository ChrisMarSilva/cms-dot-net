using Microsoft.EntityFrameworkCore;
using Rinha.Backend._2024.API.Context.Interfaces;
using Rinha.Backend._2024.API.Models.Write;
using System.Data;

namespace Rinha.Backend._2024.API.Context;

internal sealed class AppWriteDbContext : DbContext, IDataContext
{
    public AppWriteDbContext(DbContextOptions<AppWriteDbContext> options)
        : base(options) { }

    public DbSet<ClienteWriteModel> Clientes { get; set; }
    public DbSet<ClienteCarteiraWriteModel> Carteiras { get; set; }
    public DbSet<ClienteTransacaoWriteModel> Transacoes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AppWriteDbContext).Assembly, WriteConfigurationsFilter);

        base.OnModelCreating(builder);
    }

    private static bool WriteConfigurationsFilter(Type type) =>
        type.FullName?.Contains("Configurations.Write") ?? false;

    public async Task OpenConnection()
    {
        if (Database.GetDbConnection().State != ConnectionState.Open)
            try { await Database.OpenConnectionAsync(); } catch { }
    }
}
