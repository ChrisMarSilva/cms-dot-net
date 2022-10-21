using CMS.Data.Mappings;
using CMS.Domain.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CMS.Data.Contexts
{
    public class BancoDeDadosContext : DbContext
    {
        
        public DbSet<Empresa> Empresas { get; set; }

        public BancoDeDadosContext() : base("Name=DbConnNote") 
        {
            Database.SetInitializer<BancoDeDadosContext>(new CreateDatabaseIfNotExists<BancoDeDadosContext>());
            Database.Initialize(false); // false = Somente ser exeutado uma vez dentro do DbContext]
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Database.Log = comando => System.Diagnostics.Debug.WriteLine(comando);
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();// Nao colocar o nomes das tabelas no plural
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();//Disable cascade delete 
            modelBuilder.Configurations.Add<Empresa>(new EmpresaMapping());
            base.OnModelCreating(modelBuilder);
        }

    }
}
