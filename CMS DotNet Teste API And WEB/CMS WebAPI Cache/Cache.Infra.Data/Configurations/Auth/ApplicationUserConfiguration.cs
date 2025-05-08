using Cache.Domain.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cache.Infra.Data.Configurations.Auth;

internal sealed class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("TbAuth_UsuarioSistema");

        // builder.HasKey(x => x.Id).HasName("PkPessoa");

        builder.Property(x => x.IspbPsp).IsRequired().HasColumnType("numeric(8)");
        builder.Property(x => x.EhSistemaCliente).IsRequired().HasColumnType("numeric(1)");
        builder.Property(x => x.AccessFailedCount).IsRequired().HasColumnType("numeric(10)");
        builder.Property(x => x.EmailConfirmed).IsRequired().HasColumnType("numeric(1)");
        builder.Property(x => x.PhoneNumberConfirmed).IsRequired().HasColumnType("numeric(1)");
        builder.Property(x => x.TwoFactorEnabled).IsRequired().HasColumnType("numeric(1)");
        builder.Property(x => x.LockoutEnabled).IsRequired().HasColumnType("numeric(1)");
        builder.Property(x => x.RefreshToken).IsRequired(false).HasMaxLength(250);
        builder.Property(x => x.DtHrExpireRefreshToken).IsRequired(false);
        builder.Property(x => x.DtHrPasswordExpires).IsRequired();
        builder.Property(x => x.SolicAltSenha).IsRequired().HasColumnType("numeric(1)");

        builder.HasIndex(x => x.IspbPsp).IsUnique(false);
    }
}