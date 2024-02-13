using CMS_DotNet_Teste_Resquest_Http_FoxBit.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS_DotNet_Teste_Resquest_Http_FoxBit.Infrastructure.Configurations;

public class SystemTimeConfiguration : IEntityTypeConfiguration<SystemTimeModel>
{
    public void Configure(EntityTypeBuilder<SystemTimeModel> builder)
    {
        builder.ToTable("TbTnBFoxbit_SystemTime");

        builder.HasKey(x => new { x.type, x.iso }).HasName("PkTnBFoxbit_SystemTime");

        builder.Property(x => x.type).HasColumnName("type").HasMaxLength(20).IsRequired().ValueGeneratedNever();
        builder.Property(x => x.iso).HasColumnName("iso").HasMaxLength(20).IsRequired().ValueGeneratedNever();
        builder.Property(x => x.timestamp).HasColumnName("timestamp").HasPrecision(3).IsRequired();
    }
}