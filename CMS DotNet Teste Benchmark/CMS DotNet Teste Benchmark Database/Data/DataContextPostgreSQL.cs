using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TesteBenchmarkDotNet.Models;

namespace TesteBenchmarkDotNet.Data;

public sealed class DataContextPostgreSQL : DbContext
{
    public DataContextPostgreSQL() { }
    public DataContextPostgreSQL(DbContextOptions<DataContextPostgreSQL> options) : base(options) { }

    public DbSet<ClienteModel> Clientes { get; set; }
    public DbSet<ClienteCarteiraModel> Carteiras { get; set; }
    public DbSet<ClienteTransacaoModel> Transacoes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = @"Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres;Pooling=true;MinPoolSize=1;MaxPoolSize=20;CommandTimeout=60;Timeout=60;Maximum Pool Size=150;Cancellation Timeout=0;No Reset On Close=true";
            optionsBuilder.UseNpgsql(connectionString);
        }

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
