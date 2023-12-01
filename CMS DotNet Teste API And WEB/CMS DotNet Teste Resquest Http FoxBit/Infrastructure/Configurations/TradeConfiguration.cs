using CMS_DotNet_Teste_Resquest_Http_FoxBit.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS_DotNet_Teste_Resquest_Http_FoxBit.Infrastructure.Configurations;

public class TradeConfiguration : IEntityTypeConfiguration<TradeModel>
{
    public void Configure(EntityTypeBuilder<TradeModel> builder)
    {
        builder.ToTable("TbTnBFoxbit_Trades");

        builder.HasKey(x => x.id).IsClustered().HasName("PkTnBFoxbit_Trades");

        builder.Property(x => x.id).HasColumnName("id").IsRequired().ValueGeneratedNever();
        builder.Property(x => x.sn).HasColumnName("sn").HasMaxLength(20).IsRequired();
        builder.Property(x => x.client_order_id).HasColumnName("client_order_id").HasColumnType("numeric(15,0)").IsRequired();
        builder.Property(x => x.market_symbol).HasColumnName("market_symbol").HasMaxLength(20).IsRequired();
        builder.Property(x => x.side).HasColumnName("side").HasMaxLength(20).IsRequired();
        builder.Property(x => x.type).HasColumnName("type").HasMaxLength(20).IsRequired();
        builder.Property(x => x.state).HasColumnName("state").HasMaxLength(20).IsRequired();
        builder.Property(x => x.price).HasColumnName("price").HasColumnType("decimal(21,8)").IsRequired();
        builder.Property(x => x.price_avg).HasColumnName("price_avg").HasColumnType("decimal(21,8)").IsRequired(false);
        builder.Property(x => x.quantity).HasColumnName("quantity").HasColumnType("decimal(21,8)").IsRequired();
        builder.Property(x => x.quantity_executed).HasColumnName("quantity_executed").HasColumnType("decimal(21,8)").IsRequired(false);
        builder.Property(x => x.instant_amount).HasColumnName("instant_amount").HasColumnType("decimal(21,8)").IsRequired();
        builder.Property(x => x.instant_amount_executed).HasColumnName("instant_amount_executed").HasColumnType("decimal(21,8)").IsRequired(false);
        builder.Property(x => x.created_at).HasColumnName("created_at").HasPrecision(7).IsRequired();
        builder.Property(x => x.trades_count).HasColumnName("trades_count").HasColumnType("numeric(9,0)").IsRequired(false);
        builder.Property(x => x.remark).HasColumnName("remark").HasMaxLength(250).IsRequired(false);
        builder.Property(x => x.funds_received).HasColumnName("funds_received").HasColumnType("decimal(21,8)").IsRequired(false);
    }
}