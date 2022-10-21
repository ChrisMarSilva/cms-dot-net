using System;

namespace CMS.EF.Performance.Console.CNAB240.Entity
{
    public class JDArquivo
    {
        public decimal Id { get; set; } //IDARQV numeric(9, 0) NOT NULL,
        public decimal? IdRet { get; set; } //  int?// Nullable<int> //IDARQVRET numeric(9, 0) NULL,
        public string CodLegado { get; set; }//CDLEGADO varchar(10) NOT NULL,
        public string Nome { get; set; }//NMARQV varchar(100) NOT NULL,
        public string Tipo { get; set; }//TPARQV varchar(10) NOT NULL,
        public string Data { get; set; }//DTARQV varchar(8) NOT NULL,
        public decimal Seq { get; set; }//SEQARQV numeric(9, 0) NOT NULL,
        public decimal DthrGeracao { get; set; }//DTHRGERACAO numeric(14, 0) NOT NULL,
        public decimal DthrRegistro { get; set; }//DTHRREGISTRO numeric(14, 0) NOT NULL,
        public decimal QtdeProc { get; set; }//QTDPROC numeric(3, 0) NOT NULL,
        public string Situacao { get; set; }//STARQV varchar(3) NOT NULL,

    }
}
