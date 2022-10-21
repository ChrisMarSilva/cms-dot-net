using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.EF.Performance.Console.CNAB240.Enums
{

	//TBJDSPBCAB_CNAB240_ARQUIVO_ST
	//RF0 Arquivo FINAL Pendente geração.
	//RF9 Arquivo FINAL Gerado com Sucesso.
	//RFE Arquivo FINAL Gerado com erro. Pendente re-processamento.
	//RFX Falha ao Gerar Arquivo FINAL.
	//RR0 Arquivo REM Recebido com sucesso. Pendente carregar.
	//RR1 Arquivo REM Iniciado Carregamento.
	//RR2 Arquivo REM Carregado com sucesso. Pendente integração.
	//RR3 Arquivo REM Iniciado Integração.
	//RR5 Arquivo REM Integrado com Sucesso. Pendente geração Arquivo RET.
	//RR6 Arquivo REM Iniciado Resposta.
	//RR9 Arquivo REM Respondido com Sucesso.
	//RRE Arquivo REM Recebido com erro. Pendente re-processamento.
	//RRX Falha ao Receber Arquivo REM.
	//RT0 Arquivo RET Pendente geração.
	//RT9 Arquivo RET Gerado com Sucesso.
	//RTE Arquivo RET Gerado com erro. Pendente re-processamento.
	//RTX Falha ao Gerar Arquivo RET.
	
	public enum ArquivoSituacao
	{
		TpFisica = 1,
		TpJuridica = 2
	}
}
