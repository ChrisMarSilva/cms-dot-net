using Domain;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Data
{
    public class ProductDbContext : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Loja> Lojas { get; set; }
        public DbSet<Marca> Marcas { get; set; }

        public ProductDbContext() : base("Name=ProductDb")
        {
            //Database.SetInitializer<ProductDbContext>(new CreateDatabaseIfNotExists<ProductDbContext>());
            //Database.Initialize(false); // false = Somente ser exeutado uma vez dentro do DbContext
            Database.Log = d => System.Diagnostics.Debug.WriteLine(d);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();//Disable cascade delete 
        }

    }
}
