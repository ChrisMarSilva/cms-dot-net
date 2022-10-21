using CMS.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace CMS.Data.Mappings
{
    public class EmpresaMapping : EntityTypeConfiguration<Empresa>
    {
        public EmpresaMapping()
        {
            ToTable("TbEmpresa");
            HasKey(v => v.Id);
            Property(v => v.Nome).IsRequired().HasMaxLength(100);
        }
    }
}
