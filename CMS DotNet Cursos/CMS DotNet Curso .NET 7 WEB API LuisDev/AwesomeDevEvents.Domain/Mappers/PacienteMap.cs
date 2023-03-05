using AwesomeDevEvents.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AwesomeDevEvents.Domain.Mappers
{
    public class PacienteMap : BaseEntityMap<Paciente>
    {
        public PacienteMap() : base("tb_paciente") { }

        public override void Configure(EntityTypeBuilder<Paciente> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Nome).HasColumnName("nome").HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.Cpf).HasColumnName("cpf").HasColumnType("varchar(11)").IsRequired();
            builder.Property(x => x.Email).HasColumnName("email").HasColumnType("varchar(50)");
            builder.Property(x => x.Celular).HasColumnName("celular").HasColumnType("varchar(100)").IsRequired();
        }
    }
}
