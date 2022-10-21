using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace WebApplication1.Models
{
    public class TarefaContext : DbContext
    {
        public DbSet<Tarefa> Tarefas { get; set; }

        public TarefaContext() : base("Name=ProductDb")
        {
            Database.SetInitializer<TarefaContext>(new CreateDatabaseIfNotExists<TarefaContext>());
            Database.Initialize(false); // false = Somente ser exeutado uma vez dentro do DbContext
            Database.Log = comando => System.Diagnostics.Debug.WriteLine(comando);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();//Disable cascade delete 
        }
    }
}