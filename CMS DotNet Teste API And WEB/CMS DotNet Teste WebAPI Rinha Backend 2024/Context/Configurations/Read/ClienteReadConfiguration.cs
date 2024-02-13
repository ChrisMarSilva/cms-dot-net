using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rinha.Backend._2024.API.Models.Read;

namespace Rinha.Backend._2024.API.Context.Configurations.Read;

internal sealed class ClienteReadConfiguration : IEntityTypeConfiguration<ClienteReadModel>
{
    public void Configure(EntityTypeBuilder<ClienteReadModel> builder)
    {
        builder.ToTable("Cliente");

        builder.HasKey(x => x.IdCliente);

        //builder.Property(x => x.IdCliente).HasColumnName("idcliente").IsRequired(); //.ValueGeneratedOnAdd().UseIdentityColumn()
        //builder.Property(x => x.Limite).HasColumnName("limite").IsRequired();

        //SqlServerIndexBuilderExtensions.IncludeProperties(
        //    builder.HasIndex(x => x.IdCliente)
        //        .IsUnique(false)
        //        .HasDatabaseName("IxCliente01"), 
        //    x => x.Limite);
    }
}
