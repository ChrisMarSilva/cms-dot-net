using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using Tarefas.Domain.Mappers;
using Tarefas.Domain.Models;

namespace Tarefas.Data.Persistence;

// public class AppDbContext 
// {
// public delegate Task<IDbConnection> GetConnection();
// }

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Tarefa> Tarefas { get; set; }
    //public DbSet<Tarefa> Tarefas => Set<Tarefa>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Ignore<Notification>();

        // builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        // builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        // builder.ApplyConfigurationsFromAssembly(typeof(IEntityTypeConfiguration).Assembly);
        builder.ApplyConfiguration(new TarefaMap());

        //// Tarefa
        //builder.Entity<Tarefa>().HasData(new Tarefa { Atividade = "Tarefa 01", Status = "Status 01" });
        //builder.Entity<Tarefa>().HasData(new Tarefa { Atividade = "Tarefa 02", Status = "Status 02" });
        //builder.Entity<Tarefa>().HasData(new Tarefa { Atividade = "Tarefa 03", Status = "Status 03" });

        base.OnModelCreating(builder);
    }

}


