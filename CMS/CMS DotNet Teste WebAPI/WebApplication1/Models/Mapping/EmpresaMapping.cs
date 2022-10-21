using System.Data.Entity.ModelConfiguration;
using WebApplication1.Models.Entities;

namespace WebApplication1.Models.Mapping
{
    public class EmpresaMapping : EntityTypeConfiguration<Empresa>
    {
        public EmpresaMapping()
        {
            ToTable("TBEmpresa");
            HasKey(v => v.Id);
            //HasKey(u => u.Employee.EmployeeId);
            //HasKey(u => u.Project.ProjectId);
            //HasKey(u => new { u.Employee.EmployeeId, u.Project.ProjectId });
            Property(v => v.Nome).IsRequired().HasMaxLength(100)
                    .HasColumnName("NomeEmp")
                    //.HasColumnOrder(3)
                    //.HasPrecision(2, 2)
                    //.HasColumnType("Date")
                    //.HasDefaultValueSql("GetDate()");
                    .HasColumnType("varchar");
            Property(v => v.Descr).IsOptional().HasMaxLength(100);
        }
    }
}