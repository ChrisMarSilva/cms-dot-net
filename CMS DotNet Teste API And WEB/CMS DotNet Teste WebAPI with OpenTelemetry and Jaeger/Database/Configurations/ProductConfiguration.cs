using Project.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Database.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<ProductModel>
{
    public virtual void Configure(EntityTypeBuilder<ProductModel> builder)
    {
        builder.ToTable("product");

        builder.HasKey(x => x.Id).HasName("PkProduct");

        builder.Property(x => x.Id).HasColumnName("id").IsRequired().ValueGeneratedOnAdd(); 
        builder.Property(x => x.Name).HasColumnName("name").HasColumnType("varchar(150)").IsRequired();
        builder.Property(x => x.Price).HasColumnName("price").HasColumnType("decimal(17,2)").IsRequired();
        builder.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at").IsRequired(false);
    }
}