using CMS.EF.Performance.Console.CNAB240.Entity;
using System.Data.Entity.ModelConfiguration;

namespace CMS.EF.Performance.Console.CNAB240.Mappings
{
    public class JDRespostaMapping : EntityTypeConfiguration<JDResposta>
    {
        public JDRespostaMapping()
        {
            ToTable("TBJDSPBCAB_CNAB240_RESPOSTA");
            HasKey(v => v.Id).Property(v => v.Id).HasColumnName("IDRESP");
            Property(v => v.IdArqvRem).HasColumnName("IDARQV_REM").IsRequired();
            Property(v => v.SeqArqvRem).HasColumnName("SEQREG_REM").IsRequired();
            Property(v => v.NumCtrlIF).HasColumnName("NUMCTRLIF").IsOptional().HasMaxLength(20);
            Property(v => v.Data).HasColumnName("DTMOVTO").IsOptional().HasMaxLength(8);
            Property(v => v.Valor).HasColumnName("VLRFINAN").IsOptional().HasMaxLength(50);
            Property(v => v.IdArqvRem).HasColumnName("IDARQV_FINAL").IsOptional();
            Property(v => v.SitRegistro).HasColumnName("STAREG").IsRequired().HasMaxLength(3);
            Property(v => v.SitLegado).HasColumnName("ST_LEG").IsRequired().HasMaxLength(3);
            Property(v => v.DtHrRegistro).HasColumnName("DTHRREGISTRO").IsRequired();
        }
    }
}
