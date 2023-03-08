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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Ignore<Notification>();

        builder.ApplyConfiguration(new CategoriaMap());
        builder.ApplyConfiguration(new ProdutoMap());
    }

}
