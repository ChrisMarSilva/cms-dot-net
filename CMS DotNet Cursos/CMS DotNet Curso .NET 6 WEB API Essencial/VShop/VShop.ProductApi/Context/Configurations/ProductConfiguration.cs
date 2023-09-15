using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VShop.ProductApi.Models;

namespace VShop.ProductApi.Context.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<ProductModel>
{
    public virtual void Configure(EntityTypeBuilder<ProductModel> builder)
    {
        builder.ToTable("Produto");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id").IsRequired();
        builder.Property(x => x.Name).HasColumnName("nome").HasMaxLength(100).IsRequired();
        builder.Property(x => x.Price).HasColumnName("preco").HasPrecision(12, 2).IsRequired();
        builder.Property(x => x.Description).HasColumnName("descricao").HasMaxLength(255).IsRequired(false);
        builder.Property(x => x.Stock).HasColumnName("estoque").HasColumnType("float").IsRequired();
        builder.Property(x => x.ImageURL).HasColumnName("imagem_url").HasMaxLength(255).IsRequired(false);

        // builder.WithOne(c => c.Category).HasMany(x => x.Products).IsRequired().OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(p => p.CategoryId).IsRequired().OnDelete(DeleteBehavior.Cascade);
    }
}
