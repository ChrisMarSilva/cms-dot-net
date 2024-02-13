using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rinha.Backend._2024.API.Models.Write;
using System.Drawing;

namespace Rinha.Backend._2024.API.Context.Configurations.Write;

internal sealed class ClienteTransacaoWriteConfiguration : IEntityTypeConfiguration<ClienteTransacaoWriteModel>
{
    public void Configure(EntityTypeBuilder<ClienteTransacaoWriteModel> builder)
    {
        builder.ToTable("ClienteTransacao");

        builder.HasKey(x => x.IdTransacao);

        builder.Property(x => x.IdTransacao).HasColumnName("idtransacao").IsRequired(); // .ValueGeneratedOnAdd().UseIdentityColumn()
        builder.Property(x => x.IdCliente).HasColumnName("idcliente").IsRequired();
        builder.Property(x => x.Valor).HasColumnName("valor").IsRequired();
        builder.Property(x => x.Tipo).HasColumnName("tipo").HasColumnType("char(1)").IsRequired();
        builder.Property(x => x.Descricao).HasColumnName("descricao").HasMaxLength(10).IsRequired(false);
        builder.Property(x => x.DtHrRegistro).HasColumnName("dthrregistro").HasPrecision(7).IsRequired();

        //builder.HasIndex(x => x.IdCliente)
        //    .IsUnique(false)
        //    .HasDatabaseName("IxClienteTransacao01");

        //SqlServerIndexBuilderExtensions.IncludeProperties(
        //    builder.HasIndex(x => x.IdCliente)
        //        .IsUnique(false)
        //        .HasDatabaseName("IxClienteTransacao02"),
        //    x => x.DtHrRegistro);

        SqlServerIndexBuilderExtensions.IncludeProperties(
            builder.HasIndex(x => x.IdCliente)
                .IsUnique(false)
                .HasDatabaseName("IxClienteTransacao03"),
        x => new { x.DtHrRegistro, x.Tipo, x.Valor, x.Descricao });

        //builder.HasOne(x => x.Cliente)
        //    .WithMany(x => x.Transacoes)
        //    .HasForeignKey(x => x.IdCliente)
        //    .OnDelete(DeleteBehavior.Restrict)
        //    .IsRequired();
    }
}