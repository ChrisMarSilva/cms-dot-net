using Catalogo.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalogo.Domain.Mappers;

public class ProdutoMap : BaseEntityMap<Produto>
{
    public ProdutoMap() : base("Produto") { }

    public override void Configure(EntityTypeBuilder<Produto> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Nome).HasColumnName("nome").HasColumnType("varchar(100)").IsRequired();
        builder.Property(x => x.Descricao).HasColumnName("descricao").HasColumnType("varchar(150)").IsRequired(false);
        builder.Property(x => x.Preco).HasColumnName("preco").IsRequired(); // .HasColumnType("decimal(17,2)")
        builder.Property(x => x.ImagemUrl).HasColumnName("imagem_url").HasColumnType("varchar(300)").IsRequired(false);
        builder.Property(x => x.Estoque).HasColumnName("estoque").IsRequired(); // HasColumnType("decimal(17,2)").
    }
}