using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rinha.Backend._2024.API.Models.Domains.Write;

namespace Rinha.Backend._2024.API.Context.Configurations.Write;

internal sealed class ClienteCarteiraWriteConfiguration : IEntityTypeConfiguration<ClienteCarteiraWriteModel>
{
    public void Configure(EntityTypeBuilder<ClienteCarteiraWriteModel> builder)
    {
        builder.ToTable("ClienteCarteira");

        builder.HasKey(x => x.IdCliente);

        builder.Property(x => x.IdCliente).HasColumnName("idcliente").IsRequired();
        builder.Property(x => x.Saldo).HasColumnName("saldo").IsRequired();
    }
}
