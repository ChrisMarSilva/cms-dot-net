using Catalogo.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalogo.Infrastructure.Configurations;

public class ProdutoConfiguration : BaseEntityConfiguration<Produto>
{
    public ProdutoConfiguration() : base("Produto") { }

    public override void Configure(EntityTypeBuilder<Produto> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Nome).HasColumnName("nome").HasColumnType("varchar(80)").IsRequired();
        builder.Property(x => x.Descricao).HasColumnName("descricao").HasColumnType("varchar(300)").IsRequired(false);
        builder.Property(x => x.Preco).HasColumnName("preco").HasColumnType("decimal(10,2)").IsRequired();
        builder.Property(x => x.ImagemUrl).HasColumnName("imagem_url").HasColumnType("varchar(300)").IsRequired(false);
        builder.Property(x => x.Estoque).HasColumnName("estoque").HasColumnType("float").IsRequired();
        builder.Property(x => x.CategoriaId).HasColumnName("categoria_id").IsRequired();

        builder.HasOne(x => x.Categoria).WithMany(x => x.Produtos).HasForeignKey(p => p.CategoriaId);
    }
}