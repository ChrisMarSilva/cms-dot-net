using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.EF.Performance.Console.CNAB240.Enums
{
    
//TBJDSPBCAB_CNAB240_REGISTRO_TP
//	0	Header do Arquivo
//	1	Header de Lote 
//	3	Registro de Detalhe
//	5	Trailer de Lote 
//	9	Trailer do Arquivo

    public enum RegistroTipo
    {
        TpFisica = 1, //(int)RegistroTipo.TpFisica
        TpJuridica = 2
    }
}
