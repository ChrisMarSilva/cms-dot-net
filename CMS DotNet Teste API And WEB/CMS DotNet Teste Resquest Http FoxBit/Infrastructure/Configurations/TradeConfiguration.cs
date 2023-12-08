using CMS_DotNet_Teste_Resquest_Http_FoxBit.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS_DotNet_Teste_Resquest_Http_FoxBit.Infrastructure.Configurations;

public class TradeConfiguration : IEntityTypeConfiguration<TradeModel>
{
    public void Configure(EntityTypeBuilder<TradeModel> builder)
    {
        builder.ToTable("TbTnBFoxbit_Trades");

        builder.HasKey(x => x.id).HasName("PkTnBFoxbit_Trades");

        builder.Property(x => x.id).HasColumnName("id").HasColumnType("numeric(15,0)").IsRequired().ValueGeneratedNever();
        builder.Property(x => x.sn).HasColumnName("sn").HasMaxLength(20).IsRequired();
        builder.Property(x => x.order_id).HasColumnName("order_id").HasColumnType("numeric(15,0)").IsRequired();
        builder.Property(x => x.market_symbol).HasColumnName("market_symbol").HasMaxLength(20).IsRequired();
        builder.Property(x => x.side).HasColumnName("side").HasMaxLength(20).IsRequired();
        builder.Property(x => x.price).HasColumnName("price").HasColumnType("decimal(21,8)").IsRequired();
        builder.Property(x => x.quantity).HasColumnName("quantity").HasColumnType("decimal(21,8)").IsRequired();
        builder.Property(x => x.fee).HasColumnName("fee").HasColumnType("decimal(21,8)").IsRequired();
        builder.Property(x => x.fee_currency_symbol).HasColumnName("fee_currency_symbol").HasMaxLength(20).IsRequired();
        builder.Property(x => x.created_at).HasColumnName("created_at").HasPrecision(3).IsRequired();
    }
}