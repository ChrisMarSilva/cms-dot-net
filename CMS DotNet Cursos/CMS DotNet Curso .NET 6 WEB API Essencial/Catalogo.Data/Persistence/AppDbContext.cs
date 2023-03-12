using Catalogo.Domain.Mappers;
using Catalogo.Domain.Models;
using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.Data.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Produto> Produtos { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder options)
    //{
    //    options.EnableSensitiveDataLogging();
    //    options.EnableServiceProviderCaching();
    //    base.OnConfiguring(options);
    //}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Ignore<Notification>();

        // builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        // builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        // builder.ApplyConfigurationsFromAssembly(typeof(IEntityTypeConfiguration).Assembly);
        builder.ApplyConfiguration(new CategoriaMap());
        builder.ApplyConfiguration(new ProdutoMap());

        // // Categoria

        //var Categ1 = new Categoria { Nome = "Categora 01" };
        //var Categ2 = new Categoria { Nome = "Categora 02" };
        //var Categ3 = new Categoria { Nome = "Categora 03" };

        //builder.Entity<Categoria>().HasData(Categ1);
        //builder.Entity<Categoria>().HasData(Categ2);
        //builder.Entity<Categoria>().HasData(Categ3);

        // // Produto

        //var Prod1 = new Produto { Nome = "Produto 01", Preco = 10, Estoque = 100, CategoriaId = Categ1.Id };
        //var Prod2 = new Produto { Nome = "Produto 02", Preco = 20, Estoque = 200, CategoriaId = Categ2.Id };
        //var Prod3 = new Produto { Nome = "Produto 03", Preco = 30, Estoque = 300, CategoriaId = Categ3.Id };

        //builder.Entity<Produto>().HasData(Prod1);
        //builder.Entity<Produto>().HasData(Prod2);
        //builder.Entity<Produto>().HasData(Prod3);

        base.OnModelCreating(builder);
    }

}
