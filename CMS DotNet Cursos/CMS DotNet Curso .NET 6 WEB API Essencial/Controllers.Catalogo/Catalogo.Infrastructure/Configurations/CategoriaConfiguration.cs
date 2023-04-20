using Catalogo.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalogo.Infrastructure.Configurations;

public class CategoriaConfiguration : BaseEntityConfiguration<Categoria>
{
    public CategoriaConfiguration() : base("Categoria") { }

    public override void Configure(EntityTypeBuilder<Categoria> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Nome).HasColumnName("nome").HasColumnType("varchar(80)").IsRequired();
        builder.Property(x => x.ImagemUrl).HasColumnName("imagem_url").HasColumnType("varchar(300)").IsRequired(false);

        builder.HasMany(b => b.Produtos).WithOne();
    }
}