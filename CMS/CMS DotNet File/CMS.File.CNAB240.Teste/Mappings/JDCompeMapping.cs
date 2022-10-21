using CMS.EF.Performance.Console.CNAB240.Entity;
using System.Data.Entity.ModelConfiguration;

namespace CMS.EF.Performance.Console.CNAB240.Mappings
{
    public class JDCompeMapping : EntityTypeConfiguration<JDCompe>
    {
        public JDCompeMapping()
        {
            ToTable("TBJDSPBCAB_CNAB240_COMPE");
            HasKey(v => v.Codigo).Property(v => v.Codigo).HasColumnName("CDCOMPE").HasMaxLength(3);
            Property(v => v.ISPB).HasColumnName("ISPB").IsRequired().HasMaxLength(8);
        }
    }
}
