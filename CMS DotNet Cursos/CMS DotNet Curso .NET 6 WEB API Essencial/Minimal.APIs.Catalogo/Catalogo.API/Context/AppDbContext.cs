using Catalogo.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Catalogo.API.Context
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {  }


        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            // Categoria
            mb.Entity<Categoria>().HasKey(c => c.CategoriaId);
            mb.Entity<Categoria>().Property(c => c.Nome).HasMaxLength(100).IsRequired();
            mb.Entity<Categoria>().Property(c => c.Descricao).HasMaxLength(150).IsRequired(false);

            // Produto
            mb.Entity<Produto>().HasKey(c => c.ProdutoId);
            mb.Entity<Produto>().Property(c => c.Nome).HasMaxLength(100).IsRequired();
            mb.Entity<Produto>().Property(c => c.Descricao).HasMaxLength(150).IsRequired(false);
            mb.Entity<Produto>().Property(c => c.Preco).HasPrecision(14, 2).IsRequired();
            mb.Entity<Produto>().Property(c => c.Imagem).HasMaxLength(100).IsRequired(false);
            mb.Entity<Produto>().Property(c => c.DataCompra).IsRequired();
            mb.Entity<Produto>().Property(c => c.Estoque).IsRequired();

            // Relacionamento
            mb.Entity<Produto>()
                .HasOne<Categoria>(c => c.Categoria)
                    .WithMany(p => p.Produtos)
                        .HasForeignKey(c => c.CategoriaId);

            // Load Data - Seeding Simplified
            mb.Entity<Categoria>().HasData(GetCategorias());
            mb.Entity<Produto>().HasData(GetProdutos());

            base.OnModelCreating(mb);
        }

        private List<Categoria> GetCategorias()
        {
            return Enumerable
                .Range(1, 1_000)
                .Select(idx => new Categoria(idx, $"Categoria {idx}", $"Descricao da Categoria {idx}"))
                .ToList();
        }

        private List<Produto> GetProdutos()
        {
            return Enumerable
                .Range(1, 1_000)
                .Select(idx => new Produto(idx, $"Categoria {idx}", $"Descricao da Categoria {idx}", idx, $"Imagem{idx}.png", idx * 10, idx))
                .ToList();
        }

    }
}
