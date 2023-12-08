using CMS_DotNet_Teste_Resquest_Http_FoxBit_Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS_DotNet_Teste_Resquest_Http_FoxBit.Infrastructure.Configurations;

public class MemberInfoConfiguration : IEntityTypeConfiguration<MemberInfoModel>
{
    public void Configure(EntityTypeBuilder<MemberInfoModel> builder)
    {
        builder.ToTable("TbTnBFoxbit_MemberInfos");

        builder.HasKey(x => x.sn).HasName("PkTnBFoxbit_MemberInfos");

        builder.Property(x => x.sn).HasColumnName("sn").HasMaxLength(20).IsRequired().ValueGeneratedNever();
        builder.Property(x => x.email).HasColumnName("email").HasMaxLength(250).IsRequired();
        builder.Property(x => x.level).HasColumnName("level").HasColumnType("numeric(3,0)").IsRequired();
        builder.Property(x => x.created_at).HasColumnName("created_at").HasPrecision(3).IsRequired();
        builder.Property(x => x.disabled).HasColumnName("disabled").IsRequired();
    }
}
