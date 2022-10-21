using CMS.EF.Performance.Console.CNAB240.Entity;
using System.Data.Entity.ModelConfiguration;

namespace CMS.EF.Performance.Console.CNAB240.Mappings
{
    public class JDRegistroMapping : EntityTypeConfiguration<JDRegistro>
    {
        public JDRegistroMapping()
        {
            ToTable("TBJDSPBCAB_CNAB240_REGISTRO");
            HasKey(v => new { v.Id, v.Seq });
            Property(v => v.Id).HasColumnName("IDARQV").IsRequired();
            Property(v => v.Seq).HasColumnName("SEQREG").IsRequired();
            Property(v => v.Tipo).HasColumnName("TPAREG").IsRequired().HasMaxLength(2);
            Property(v => v.NumCtrlIF).HasColumnName("NUMCTRLIF").IsOptional().HasMaxLength(20);
            Property(v => v.LinhaSegA).HasColumnName("LINHA_SEGMENTO_A").IsRequired().HasMaxLength(240);
            Property(v => v.LinhaSegB).HasColumnName("LINHA_SEGMENTO_B").IsOptional().HasMaxLength(240);
            Property(v => v.ISPBIFCred).HasColumnName("ISPBIFCRED").IsOptional().HasMaxLength(8);
            Property(v => v.Situacao).HasColumnName("STAREG").IsRequired().HasMaxLength(3);
        }
    }
}
