using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Models;

namespace Project.Database.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<ClientModel>
{
    public virtual void Configure(EntityTypeBuilder<ClientModel> builder)
    {
        builder.ToTable("client");

        builder.HasKey(x => x.Id).HasName("PkClient");

        builder.Property(x => x.Id).HasColumnName("id").IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.Name).HasColumnName("name").HasColumnType("varchar(150)").IsRequired();
        builder.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at").IsRequired(false);
    }
}