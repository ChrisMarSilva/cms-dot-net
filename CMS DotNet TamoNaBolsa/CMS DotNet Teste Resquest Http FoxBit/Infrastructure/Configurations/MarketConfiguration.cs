using CMS_DotNet_Teste_Resquest_Http_FoxBit.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS_DotNet_Teste_Resquest_Http_FoxBit.Infrastructure.Configurations;

public class MarketConfiguration : IEntityTypeConfiguration<MarketModel>
{
    public void Configure(EntityTypeBuilder<MarketModel> builder)
    {
        builder.ToTable("TbTnBFoxbit_Markets");

        builder.HasKey(x => x.symbol).HasName("PkTnBFoxbit_Markets");

        builder.Property(x => x.symbol).HasColumnName("symbol").HasMaxLength(20).IsRequired().ValueGeneratedNever();
        builder.Property(x => x.quantity_min).HasColumnName("quantity_min").HasColumnType("decimal(21,8)").IsRequired();
        builder.Property(x => x.quantity_increment).HasColumnName("quantity_increment").HasColumnType("decimal(21,8)").IsRequired();
        builder.Property(x => x.price_min).HasColumnName("price_min").HasColumnType("decimal(21,8)").IsRequired();
        builder.Property(x => x.price_increment).HasColumnName("price_increment").HasColumnType("decimal(21,8)").IsRequired();
        builder.Property(x => x.base_name).HasColumnName("base_name").HasMaxLength(100).IsRequired();
        builder.Property(x => x.base_precision).HasColumnName("base_precision").HasColumnType("numeric(3,0)").IsRequired();
        builder.Property(x => x.base_symbol).HasColumnName("base_symbol").HasMaxLength(20).IsRequired();
        builder.Property(x => x.base_type).HasColumnName("base_type").HasMaxLength(20).IsRequired();
        builder.Property(x => x.quote_name).HasColumnName("quote_name").HasMaxLength(100).IsRequired();
        builder.Property(x => x.quote_precision).HasColumnName("quote_precision").HasColumnType("numeric(3,0)").IsRequired();
        builder.Property(x => x.quote_symbol).HasColumnName("quote_symbol").HasMaxLength(20).IsRequired();
        builder.Property(x => x.quote_type).HasColumnName("quote_type").HasMaxLength(20).IsRequired();
    }
}