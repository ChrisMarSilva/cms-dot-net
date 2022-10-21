using System.Data.Entity.ModelConfiguration;
using WebApplication1.Models.Entities;

namespace WebApplication1.Models.Mapping
{
    public class VagaMapping : EntityTypeConfiguration<Vaga>
    {
        public VagaMapping()
        {
            ToTable("TbVaga");
            HasKey(v => v.Id);
            Property(v => v.Titulo).IsRequired().HasMaxLength(100);
            Property(v => v.Descricao).IsRequired().HasMaxLength(200);
            Property(v => v.Salario).IsRequired();
            Property(v => v.DataCadastro).IsRequired();
            Property(v => v.LocalTrabalho).IsRequired().HasMaxLength(50);
            HasRequired<Empresa>(v => v.Anunciante).WithMany(e => e.Vagas).HasForeignKey(v => v.EmpresaId).WillCascadeOnDelete();
        }
    }
}