using Cache.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cache.Infra.Data.Configurations;

internal sealed class PessoaModelConfiguration : IEntityTypeConfiguration<PessoaModel>
{
    public void Configure(EntityTypeBuilder<PessoaModel> builder)
    {
        builder.ToTable("Tb_Pessoa", tb => tb.HasComment("Informações de controle da requisiçao da autorização."));

        builder.HasKey(x => x.Id).HasName("PkPessoa");

        builder.Property(x => x.Id).ValueGeneratedNever().IsRequired().HasComment("Identificação do registro.");
        builder.Property(x => x.Nome).HasMaxLength(140).IsRequired().HasComment("Nome do registro.");
        builder.Property(x => x.DtHrRegistro).IsRequired().HasComment("Data e hora da criação do registro.");
    }
}
