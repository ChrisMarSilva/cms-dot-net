using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rinha.Backend._2024.API.Models.Read;

namespace Rinha.Backend._2024.API.Context.Configurations.Read;

internal sealed class ClienteCarteiraReadConfiguration : IEntityTypeConfiguration<ClienteCarteiraReadModel>
{
    public void Configure(EntityTypeBuilder<ClienteCarteiraReadModel> builder)
    {
        builder.ToTable("ClienteCarteira");

        builder.HasKey(x => x.IdCliente);

        //builder.Property(x => x.IdCliente).HasColumnName("idcliente").IsRequired();// .ValueGeneratedOnAdd().UseIdentityColumn()
        //builder.Property(x => x.Saldo).HasColumnName("saldo").IsRequired();

        //SqlServerIndexBuilderExtensions.IncludeProperties(
        //    builder.HasIndex(x => x.IdCliente)
        //        .IsUnique(false)
        //        .HasDatabaseName("IxClienteCarteira01"), 
        //    x => x.Saldo);

        //builder.HasOne(x => x.Cliente)
        //    .WithOne(x => x.Carteira)
        //    .HasForeignKey<ClienteCarteiraModel>(x => x.IdCliente)
        //    .OnDelete(DeleteBehavior.Restrict)
        //    .IsRequired();
    }
}
