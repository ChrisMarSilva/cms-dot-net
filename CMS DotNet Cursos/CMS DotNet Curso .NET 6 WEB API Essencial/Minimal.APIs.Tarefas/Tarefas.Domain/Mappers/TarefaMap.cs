using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tarefas.Domain.Models;

namespace Tarefas.Domain.Mappers;

public class TarefaMap : BaseEntityMap<Tarefa>
{
    public TarefaMap() : base("Tarefa") { }

    public override void Configure(EntityTypeBuilder<Tarefa> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Atividade).HasColumnName("atividade").HasColumnType("varchar(255)").IsRequired();
        builder.Property(x => x.Status).HasColumnName("status").HasColumnType("varchar(100)").IsRequired();
    }
}