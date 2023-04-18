using Catalogo.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalogo.Domain.Mappers;

public class AlunoMap : BaseEntityMap<Aluno>
{
    public AlunoMap() : base("Aluno") { }

    public override void Configure(EntityTypeBuilder<Aluno> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Nome).HasColumnName("nome").HasColumnType("varchar(80)").IsRequired();
        builder.Property(x => x.Email).HasColumnName("email").HasColumnType("varchar(300)").IsRequired();
        builder.Property(x => x.Idade).HasColumnName("idade").IsRequired();
    }
}