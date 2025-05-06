using Cache.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cache.Infra.Data.Configurations;

internal sealed class PessoaSituacaoModelConfiguration : IEntityTypeConfiguration<PessoaSituacaoModel>
{
    public void Configure(EntityTypeBuilder<PessoaSituacaoModel> builder)
    {
        builder.ToTable("Tb_PessoaSituacao", tb => tb.HasComment("Situações."));

        builder.HasKey(x => x.IdSituacao).HasName("PkPessoaSituacao");

        builder.Property(x => x.IdSituacao).IsRequired().ValueGeneratedNever().HasComment("Identificação da situação.");
        builder.Property(x => x.IdPessoa).IsRequired().ValueGeneratedNever().HasComment("Identificação da pessoa.");
        builder.Property(x => x.Situacao).IsRequired().HasColumnType("numeric(1,0)").HasComment("Situação.");
        builder.Property(x => x.CdErro).IsRequired(false).HasMaxLength(100).HasComment("Código do erro.");
        builder.Property(x => x.TxtErro).IsRequired(false).HasMaxLength(4000).HasComment("Texto do erro.");
        builder.Property(x => x.DtHrRegistro).IsRequired().HasComment("Data e hora da criação do registro.");

        builder.HasOne(x => x.Pessoa)
            .WithMany(x => x.Situacoes)
            .HasForeignKey(x => x.IdPessoa)
            .HasConstraintName("FkPessoaSituacao_PesSit2Pes")
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasIndex(x => x.IdPessoa)
            .IsUnique(false)
            .HasDatabaseName("IxPessoaSituacao_PesSit_01");
    }
}