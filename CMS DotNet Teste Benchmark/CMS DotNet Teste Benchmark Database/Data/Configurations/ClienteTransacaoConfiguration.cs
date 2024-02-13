using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteBenchmarkDotNet.Models;

namespace Rinha.Backend._2024.API.Context.Configurations.Read;

public sealed class ClienteTransacaoConfiguration : IEntityTypeConfiguration<ClienteTransacaoModel>
{
    public void Configure(EntityTypeBuilder<ClienteTransacaoModel> builder)
    {
        builder.ToTable("ClienteTransacao");

        builder.HasKey(x => x.IdTransacao);

        builder.Property(x => x.IdTransacao).HasColumnName("idtransacao").UseIdentityColumn().IsRequired();
        builder.Property(x => x.IdCliente).HasColumnName("idcliente").IsRequired();
        builder.Property(x => x.Valor).HasColumnName("valor").IsRequired();
        builder.Property(x => x.Tipo).HasColumnName("tipo").HasColumnType("char(1)").IsRequired();
        builder.Property(x => x.Descricao).HasColumnName("descricao").HasMaxLength(10).IsRequired();
        builder.Property(x => x.DtHrRegistro).HasColumnName("dthrregistro").HasPrecision(7).IsRequired();

        builder.HasIndex(x => x.IdCliente)
            .IsUnique(false)
            .HasDatabaseName("IxClienteTransacao01");

        builder.HasOne(x => x.Cliente)
            .WithMany(x => x.Transacoes)
            .HasForeignKey(x => x.IdCliente)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}