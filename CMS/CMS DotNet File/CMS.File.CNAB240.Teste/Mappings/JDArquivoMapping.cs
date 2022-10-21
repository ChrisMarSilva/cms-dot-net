using CMS.EF.Performance.Console.CNAB240.Entity;
using System.Data.Entity.ModelConfiguration;

namespace CMS.EF.Performance.Console.CNAB240.Mappings
{
    public class JDArquivoMapping : EntityTypeConfiguration<JDArquivo>
    {
        public JDArquivoMapping()
        {
            ToTable("TBJDSPBCAB_CNAB240_ARQUIVO");
            HasKey(v => v.Id);
            Property(v => v.Id).HasColumnName("IDARQV").IsRequired();
            Property(v => v.IdRet).HasColumnName("IDARQVRET").IsOptional();
            Property(v => v.CodLegado).HasColumnName("CDLEGADO").IsRequired().HasMaxLength(10);
            Property(v => v.Nome).HasColumnName("NMARQV").IsRequired().HasMaxLength(100);
            Property(v => v.Tipo).HasColumnName("TPARQV").IsRequired().HasMaxLength(10);
            Property(v => v.Data).HasColumnName("DTARQV").IsRequired().HasMaxLength(8);
            Property(v => v.Seq).HasColumnName("SEQARQV").IsRequired();
            Property(v => v.DthrGeracao).HasColumnName("DTHRGERACAO").IsRequired();
            Property(v => v.DthrRegistro).HasColumnName("DTHRREGISTRO").IsRequired();
            Property(v => v.QtdeProc).HasColumnName("QTDPROC").IsRequired();
            Property(v => v.Situacao).HasColumnName("STARQV").IsRequired().HasMaxLength(3);
        }
    }
}
