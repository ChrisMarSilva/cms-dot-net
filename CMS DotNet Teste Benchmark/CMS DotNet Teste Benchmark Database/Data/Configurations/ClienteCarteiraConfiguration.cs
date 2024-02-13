using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteBenchmarkDotNet.Models;

namespace TesteBenchmarkDotNet.Data.Configurations;

public sealed class ClienteCarteiraConfiguration : IEntityTypeConfiguration<ClienteCarteiraModel>
{
    public void Configure(EntityTypeBuilder<ClienteCarteiraModel> builder)
    {
        builder.ToTable("ClienteCarteira");

        builder.HasKey(x => x.IdCliente);

        builder.Property(x => x.IdCliente).HasColumnName("idcliente").IsRequired();
        builder.Property(x => x.Saldo).HasColumnName("saldo").IsRequired();

        builder.HasOne(x => x.Cliente)
            .WithOne(x => x.Carteira)
            .HasForeignKey<ClienteCarteiraModel>(x => x.IdCliente)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}
