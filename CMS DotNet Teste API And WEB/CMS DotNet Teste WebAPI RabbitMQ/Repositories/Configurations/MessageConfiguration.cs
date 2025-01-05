using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RabbitMQ.Models.Entities;

namespace RabbitMQ.Repositories.Configurations;

internal sealed class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable("TbMessage", tb => tb.HasComment("Informações de controle da Message."));

        builder.HasKey(x => x.Id).HasName("PkMessage");//.IsClustered(false);

        builder.Property(x => x.Id).ValueGeneratedNever().IsRequired().HasComment("Identificação única da mensagem.");
        builder.Property(x => x.Texto).IsRequired().HasMaxLength(4000).HasComment("Texto da mensagem.");
        builder.Property(x => x.DtHrRegistro).IsRequired().HasComment("Data e hora da criação do registro.");
    }
}