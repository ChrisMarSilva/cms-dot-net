using Cache.Domain.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cache.Infra.Data.Configurations.Auth;

internal sealed class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.ToTable("TbAuth_Funcao");

        // builder.HasKey(x => x.Id).HasName("PkPessoa");

        builder.Property(x => x.DisplayName).HasColumnType("varchar(256)");
        builder.Property(x => x.IsSystem).IsRequired().HasDefaultValue(false).HasColumnType("numeric(1)");
    }
}