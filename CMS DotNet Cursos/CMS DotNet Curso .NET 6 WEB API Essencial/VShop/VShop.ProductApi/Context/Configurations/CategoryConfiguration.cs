using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VShop.ProductApi.Models;

namespace VShop.ProductApi.Context.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<CategoryModel>
{
    public virtual void Configure(EntityTypeBuilder<CategoryModel> builder)
    {
        builder.ToTable("Categoria");

        builder.HasKey(x => x.CategoryId);

        builder.Property(x => x.CategoryId).HasColumnName("id").IsRequired();
        builder.Property(x => x.Name).HasColumnName("nome").HasMaxLength(100).IsRequired();

        builder.HasMany(b => b.Products).WithOne();
    }
}
