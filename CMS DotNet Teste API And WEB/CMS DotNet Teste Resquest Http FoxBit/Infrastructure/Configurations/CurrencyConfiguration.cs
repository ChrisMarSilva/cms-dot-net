using CMS_DotNet_Teste_Resquest_Http_FoxBit.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS_DotNet_Teste_Resquest_Http_FoxBit_Api.Context.Configurations;

public class CurrencyConfiguration : IEntityTypeConfiguration<CurrencyModel>
{
    public void Configure(EntityTypeBuilder<CurrencyModel> builder)
    {
        builder.ToTable("TbTnBFoxbit_Currencies");

        builder.HasKey(x => x.symbol).HasName("PkTnBFoxbit_Currencies");

        builder.Property(x => x.symbol).HasColumnName("symbol").HasMaxLength(20).IsRequired().ValueGeneratedNever();
        builder.Property(x => x.name).HasColumnName("name").HasMaxLength(100).IsRequired();
        builder.Property(x => x.precision).HasColumnName("precision").HasColumnType("numeric(3,0)").IsRequired(false);
        builder.Property(x => x.category_code).HasColumnName("category_code").HasMaxLength(50).IsRequired();
        builder.Property(x => x.category_name).HasColumnName("category_name").HasMaxLength(50).IsRequired();
        builder.Property(x => x.type).HasColumnName("type").HasMaxLength(20).IsRequired();
        builder.Property(x => x.deposit_min_to_confirm).HasColumnName("deposit_min_to_confirm").HasColumnType("numeric(3,0)").IsRequired();
        builder.Property(x => x.deposit_min_amount).HasColumnName("deposit_min_amount").HasColumnType("decimal(21,8)").IsRequired();
        builder.Property(x => x.withdraw_enabled).HasColumnName("withdraw_enabled").HasColumnType("numeric(1,0)").IsRequired(false);
        builder.Property(x => x.withdraw_min_amount).HasColumnName("withdraw_min_amount").HasColumnType("decimal(21,8)").IsRequired();
        builder.Property(x => x.withdraw_fee).HasColumnName("withdraw_fee").HasColumnType("decimal(21,8)").IsRequired();
    }
}