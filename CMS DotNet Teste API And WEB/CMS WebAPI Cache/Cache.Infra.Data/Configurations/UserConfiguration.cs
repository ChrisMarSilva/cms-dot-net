using Cache.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cache.Infra.Data.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<UserModel>
{
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
        builder.ToTable("TbUser", tb => tb.HasComment("Informações de controle do usuario."));

        builder.HasKey(x => x.Id).HasName("PkUser");//.IsClustered(false);

        builder.Property(x => x.Id).ValueGeneratedNever().IsRequired().HasComment("Identificação única do usuario.");
        builder.Property(x => x.Name).IsRequired().HasMaxLength(250).HasComment("Nome do usuario.");
        builder.Property(x => x.Email).IsRequired().HasMaxLength(250).HasComment("E-mail do usuario.");
        builder.Property(x => x.Password).IsRequired().HasMaxLength(250).HasComment("Senha do usuario.");
        builder.Property(x => x.DtHrCreated).IsRequired().HasComment("Data e hora da criação do registro.");
    }
}