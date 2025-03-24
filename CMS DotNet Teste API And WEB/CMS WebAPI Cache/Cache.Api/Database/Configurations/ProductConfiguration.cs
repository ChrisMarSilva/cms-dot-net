using Cache.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cache.Api.Database.Configurations;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<ProductModel>
{
    public void Configure(EntityTypeBuilder<ProductModel> builder)
    {
        builder.ToTable("TbProduct", tb => tb.HasComment("Informações de controle do produto."));

        builder.HasKey(x => x.Id).HasName("PkProduct");//.IsClustered(false);

        builder.Property(x => x.Id).ValueGeneratedNever().IsRequired().HasComment("Identificação única do produto.");
        builder.Property(x => x.Name).IsRequired().HasMaxLength(250).HasComment("Nome do produto.");
        builder.Property(x => x.Description).IsRequired().HasMaxLength(250).HasComment("Descrição do produto.");
        builder.Property(x => x.Price).IsRequired().HasComment("Preço do produto.");
    }
}