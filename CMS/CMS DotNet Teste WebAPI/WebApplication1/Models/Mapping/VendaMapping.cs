using System.Data.Entity.ModelConfiguration;
using WebApplication1.Models.Entities;

namespace WebApplication1.Models.Mapping
{
    public class VendaMapping : EntityTypeConfiguration<Venda>
    {
        public VendaMapping()
        {
            ToTable("TbVenda");
            HasKey(v => v.Id);
        }
    }
}