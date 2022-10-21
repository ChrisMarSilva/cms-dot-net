using System;

namespace CMS.EF.Performance.Console.CNAB240.Entity
{
    public class JDResposta
    {
        public decimal Id { get; set; }     //IDRESP numeric(9, 0) IDENTITY(1,1) NOT NULL,
        public decimal IdArqvRem { get; set; } //IDARQV_REM numeric(9, 0) NOT NULL,
        public decimal SeqArqvRem { get; set; } //SEQREG_REM numeric(9, 0) NOT NULL,
        public string NumCtrlIF { get; set; } //NUMCTRLIF varchar(20) NULL,
        public string Data { get; set; } //DTMOVTO varchar(8) NULL,
        public string Valor { get; set; } //VLRFINAN varchar(50) NULL,
        public decimal IdArqvFinal { get; set; } //IDARQV_FINAL numeric(9, 0) NULL,
        public string SitRegistro { get; set; } //STAREG varchar(3) NOT NULL,
        public string SitLegado { get; set; } //ST_LEG varchar(3) NOT NULL,
        public string CodLegado { get; set; } //CDLEGADO varchar(10) NOT NULL,
        public decimal DtHrRegistro { get; set; } //DTHRREGISTRO numeric(14, 0) NOT NULL,
    }
}
