using Core.CMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.CMS.Data.Mappings
{

    public class EmpresaMapping : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.ToTable("TbEmpresa");
            builder.HasKey(x => x.Id).HasName("PkEmpresa");
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(100);
        }
    }
}
