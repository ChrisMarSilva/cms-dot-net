using System.Data.Entity.ModelConfiguration;
using WebApplication1.Models.Entities;

namespace WebApplication1.Models.Mapping
{
    public class ItemVendaMapping : EntityTypeConfiguration<ItemVenda>
    {
        public ItemVendaMapping()
        {
            ToTable("TbItemVenda");
            HasKey(v => v.Id);
        }
    }
}