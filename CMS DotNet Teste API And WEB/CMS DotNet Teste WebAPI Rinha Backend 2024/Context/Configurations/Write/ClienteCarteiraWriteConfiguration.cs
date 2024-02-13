using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rinha.Backend._2024.API.Models.Write;

namespace Rinha.Backend._2024.API.Context.Configurations.Write;

internal sealed class ClienteCarteiraWriteConfiguration : IEntityTypeConfiguration<ClienteCarteiraWriteModel>
{
    public void Configure(EntityTypeBuilder<ClienteCarteiraWriteModel> builder)
    {
        builder.ToTable("ClienteCarteira");

        builder.HasKey(x => x.IdCliente);

        builder.Property(x => x.IdCliente).HasColumnName("idcliente").IsRequired();// .ValueGeneratedOnAdd().UseIdentityColumn()
        builder.Property(x => x.Saldo).HasColumnName("saldo").IsRequired();

        //builder.HasOne(x => x.Cliente)
        //    .WithOne(x => x.Carteira)
        //    .HasForeignKey<ClienteCarteiraModel>(x => x.IdCliente)
        //    .OnDelete(DeleteBehavior.Restrict)
        //    .IsRequired();
    }
}
