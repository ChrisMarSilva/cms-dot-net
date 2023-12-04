using CMS_DotNet_Teste_Resquest_Http_FoxBit.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS_DotNet_Teste_Resquest_Http_FoxBit_Api.Context.Configurations;

public class MarketQuoteConfiguration : IEntityTypeConfiguration<MarketQuoteModel>
{
    public void Configure(EntityTypeBuilder<MarketQuoteModel> builder)
    {
        builder.ToTable("TbTnBFoxbit_MarketQuotes");

        builder.HasKey(x => x.market_symbol).HasName("PkTnBFoxbit_MarketQuotes");

        builder.Property(x => x.market_symbol).HasColumnName("market_symbol").HasMaxLength(30).IsRequired().ValueGeneratedNever();
        builder.Property(x => x.side).HasColumnName("side").HasMaxLength(20).IsRequired();
        builder.Property(x => x.base_amount).HasColumnName("base_amount").HasColumnType("decimal(21,8)").IsRequired();
        builder.Property(x => x.quote_amount).HasColumnName("quote_amount").HasColumnType("numeric(3,0)").IsRequired();
        builder.Property(x => x.price).HasColumnName("price").HasColumnType("decimal(21,8)").IsRequired();
    }
}