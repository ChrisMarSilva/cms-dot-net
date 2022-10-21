using System;

namespace CMS.EF.Performance.Console.CNAB240.Entity
{
    public class JDRegistro
    {
        public decimal Id { get; set; }  //	IDARQV numeric(9, 0) NOT NULL,
        public decimal Seq { get; set; }//	SEQREG numeric(9, 0) NOT NULL,
        public string Tipo { get; set; }//	TPAREG varchar(2) NOT NULL,
        public string NumCtrlIF { get; set; }//	NUMCTRLIF varchar(20) NULL,
        public string LinhaSegA { get; set; }//LINHA_SEGMENTO_A varchar(240) NOT NULL,
        public string LinhaSegB { get; set; }//LINHA_SEGMENTO_B varchar(240) NULL,
        public string ISPBIFCred { get; set; }//ISPBIFCRED varchar(8) NULL,
        public string Situacao { get; set; }//STAREG varchar(3) NOT NULL,

    }
}
