using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using WebApplication1.Models.Entities;
using WebApplication1.Models.Mapping;

namespace WebApplication1.Models.Context
{
    public class BancoContext : DbContext
    {

        public BancoContext() : base("Name=ProductDb")
        {
            // Database.SetInitializer<DevStoreDataContext>(new DevStoreDataContextInitializer());
            Database.SetInitializer<BancoContext>(new CreateDatabaseIfNotExists<BancoContext>());
            Database.Initialize(false); // false = Somente ser exeutado uma vez dentro do DbContext]
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Database.Log = comando => System.Diagnostics.Debug.WriteLine(comando);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //   // optionsBuilder.UseSqlServer(this._config.GetConnectionString("StoreConn"));
        //}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           // modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();//Disable cascade delete 
            modelBuilder.Configurations.Add<Vaga>(      new VagaMapping()      );
            modelBuilder.Configurations.Add<Empresa>(   new EmpresaMapping()   );
            modelBuilder.Configurations.Add<Requisito>( new RequisitoMapping() );
            modelBuilder.Configurations.Add<Venda>(     new VendaMapping()     );
            modelBuilder.Configurations.Add<ItemVenda>( new ItemVendaMapping() );
            modelBuilder.Configurations.Add<Product>(   new ProductMap()       );
            modelBuilder.Configurations.Add<Category>(  new CategoryMap()      );

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Tarefa> Tarefas { get; set; }

        public DbSet<Curso> Cursos { get; set; }

        public DbSet<Aula> Aulas { get; set; }

        public DbSet<Vaga> Vagas { get; set; }

        public DbSet<Empresa> Empresas { get; set; }

        public DbSet<Requisito> Requisitos { get; set; }

        public DbSet<Venda> Vendas { get; set; }

        public DbSet<ItemVenda> ItensVenda { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        

    }
    public class DevStoreDataContextInitializer : DropCreateDatabaseIfModelChanges<BancoContext>
    {
        protected override void Seed(BancoContext context)
        {
            //context.Categories.Add(new Category { Id = 1, Title = "Informática" });
            //context.Categories.Add(new Category { Id = 2, Title = "Games" });
            //context.Categories.Add(new Category { Id = 3, Title = "Papelaria" });
            //context.SaveChanges();

            //context.Products.Add(new Product { Id = 1, CategoryId = 1, IsActive = true, Title = "Mouse Microsoft Confort 5000", Price = 99 });
            //context.Products.Add(new Product { Id = 2, CategoryId = 1, IsActive = true, Title = "Teclado Microsoft Confort 5000", Price = 199 });
            //context.Products.Add(new Product { Id = 3, CategoryId = 1, IsActive = true, Title = "Mouse Pad Razor", Price = 59 });

            //context.Products.Add(new Product { Id = 4, CategoryId = 2, IsActive = true, Title = "Gears of War", Price = 59 });
            //context.Products.Add(new Product { Id = 5, CategoryId = 2, IsActive = true, Title = "Gears of War 2", Price = 79 });
            //context.Products.Add(new Product { Id = 6, CategoryId = 2, IsActive = true, Title = "Gears of War 3", Price = 99 });

            //context.Products.Add(new Product { Id = 7, CategoryId = 3, IsActive = true, Title = "Papel Sulfite 1000 folhas", Price = 9.89M });
            //context.SaveChanges();

            base.Seed(context);
        }
    }
}