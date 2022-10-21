using System.Data.Entity.ModelConfiguration;
using WebApplication1.Models.Entities;

namespace WebApplication1.Models.Mapping
{
    public class RequisitoMapping : EntityTypeConfiguration<Requisito>
    {
        public RequisitoMapping()
        {
            ToTable("TbRequisito");
            HasKey(v => v.Id);
            Property(v => v.Descricao).IsRequired().HasMaxLength(100);
            HasRequired<Vaga>(r => r.Vaga).WithMany(v => v.Requisitos).WillCascadeOnDelete();
        }
    }
}