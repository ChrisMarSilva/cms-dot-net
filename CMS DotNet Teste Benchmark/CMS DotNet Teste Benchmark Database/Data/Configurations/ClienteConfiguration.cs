using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteBenchmarkDotNet.Models;

namespace Rinha.Backend._2024.API.Context.Configurations.Read;

public sealed class ClienteConfiguration : IEntityTypeConfiguration<ClienteModel>
{
    public void Configure(EntityTypeBuilder<ClienteModel> builder)
    {
        builder.ToTable("Cliente");

        builder.HasKey(x => x.IdCliente);

        builder.Property(x => x.IdCliente).HasColumnName("idcliente").IsRequired(); //.UseIdentityColumn().ValueGeneratedOnAdd()
        builder.Property(x => x.Limite).HasColumnName("limite").IsRequired();
    }
}
