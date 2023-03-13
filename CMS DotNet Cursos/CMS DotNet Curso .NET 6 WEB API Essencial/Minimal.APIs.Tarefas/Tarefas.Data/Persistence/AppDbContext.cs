using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Tarefas.Domain.Mappers;
using Tarefas.Domain.Models;

namespace Tarefas.Data.Persistence;

public class AppDbContextCon
{
    public delegate Task<IDbConnection> GetConnection();
}

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

        // Tarefa

        //builder.Entity<Tarefa>().HasData(new Tarefa { Atividade = "Tarefa 01", Status = "Status 01" });
        //builder.Entity<Tarefa>().HasData(new Tarefa { Atividade = "Tarefa 02", Status = "Status 02" });
        //builder.Entity<Tarefa>().HasData(new Tarefa { Atividade = "Tarefa 03", Status = "Status 03" });

        //if (!Tarefas.Any())
        //{ 
        //    var tarefas = Enumerable
        //        .Range(1, 10)
        //        .Select(t => new Tarefa(atividade: $"Tarefa {t}", status: "A"))
        //        .ToArray();
        //    Tarefas.AddRange(tarefas);
        //    SaveChanges();
        //}

        //var tarefas = new List<Tarefa>
        //{
        //    new Tarefa(atividade: $"Tarefa 001", status: "A"),
        //    new Tarefa(atividade: $"Tarefa 002", status: "A")
        //};
        //tarefas.ForEach(t => ctx.Tarefas.Add(t));
        //ctx.SaveChanges();

        //builder.Entity<Tarefa>().HasData(
        //    new Tarefa(atividade: $"Tarefa 001", status: "A"),
        //    new Tarefa(atividade: $"Tarefa 002", status: "A")
        //);

        base.OnModelCreating(builder);
    }

}
