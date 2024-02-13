using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rinha.Backend._2024.API.Models.Domains.Write;

namespace Rinha.Backend._2024.API.Context.Configurations.Write;

internal sealed class ClienteWriteConfiguration : IEntityTypeConfiguration<ClienteWriteModel>
{
    public void Configure(EntityTypeBuilder<ClienteWriteModel> builder)
    {
        builder.ToTable("Cliente");

        builder.HasKey(x => x.IdCliente);

        builder.Property(x => x.IdCliente).HasColumnName("idcliente").IsRequired();
        builder.Property(x => x.Limite).HasColumnName("limite").IsRequired();
    }
}
