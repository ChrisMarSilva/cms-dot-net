using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.EF.Performance.Console.CNAB240.Enums
{
//TBJDSPBCAB_CNAB240_REGISTRO_ST
//	00	Mensagem Efetivada
//	01	Mensagem não Gerada
//	02	Mensagem Duplicada
//	03	Mensagem Rejeitada Bacen
//	04	Conta Destinatária do Crédito Encerrada
//	05	Agência ou Conta Destinatária do Crédito Inválida
//	06	Ausência ou Divergência na Indicação do CPF/CNPJ
//	07	Mensagem Inválida para o Tipo de Transação ou Finalidade.
//	08	Divergência na Titularidade 
//	09	Transferência Insuficiente para Finalidade Indicada
//	10	Diferença a Maior
//	11	Código Identificador de Transferência Inválido
//	12	Devolução por Fraude
//	13	Identificação Depósito Judicial Inválida
//	14	Mensagem STR0020/PAG0116 fora do horário definido para o negócio
//	15	Número de contrato inválido
//	16	Valor em duplicidade
//	17	Movimentações financeiras ligadas ao terrorismo e ao seu financiamento
//	18	Documento de FGTS não apresentado para autenticação 
//	19	Devolução de Pagamentos efetuados pelo Tesouro
//	20	Devolução de Ordem Bancária pelo Agente Financeiro
//	21	Erro no preenchimento da devolução
//	22	Erro no Preenchimento do Documento de Recolhimento
//	23	Erro no Preenchimento do Depósito Direto
//	24	Devolução de pagamento de tributos por solicitação da IF 
//	25	Devolução de Recolhimento a Maior autorizada pela RFB
//	26	Crédito não sacado - decurso de prazo estipulado
//	27	Código de Moeda Inválido - Documentos com código de barras
//	28	Boleto de Pagamento liquidado por valor a maior ou menor - Documentos com código de barras 
//	29	Boleto de Pagamento recebido após o vencimento sem juros e demais encargos - Documentos com código de barras
//	30	Apresentação indevida - Documentos com código de barras 
//	31	Transferência supera limite para o tipo de conta destino 
//	32	Código de barras em desacordo com as especificações - Documentos com código de barras
//	33	Boleto de Pagamento já liquidado
//	34	Boletos de Pagamento liquidados em duplicidade no mesmo dia - Documentos com código de barras
//	35	Por solicitação de cliente da Instituição Participante Recebedora
//	36	Boleto de pagamento recebido com desconto ou abatimento não previsto - Documentos com código de barras
//	37	Não conformidade no pagamento
//	38	Beneficiário não identificado - Documentos com código de barras
//	39	CPF/CNPJ do Beneficiário inválido ou não confere com registro de boleto na base da IF Destinatária - Documentos com código de barras
//	40	CPF/CNPJ do Pagador inválido ou não confere com registro do boleto na base da IF Destinatária - Documentos com código de barras 
//	41	Cópia não encaminhada pela IF recebedora no prazo previsto - Documentos com código de barras
//	42	Boleto em cartório ou protestado - Documentos com código de barras
//	43	Portabilidade não registrada na central de transferência de crédito
//	44	Boleto de Pagamento divergente da Base Centralizada de Boletos de Pagamento
//	45	Boleto inexistente na Base Centralizada de Boletos de Pagamento
//	AF0 Pendente Geração do Arquivo Final
//	AF1 Iniciado Geração do Arquivo Final
//	AF9 Gerado Arquivo Final com Sucesso
//	IE Inicio Envio MQ
//	LA Mensagem Gerada
//   PD  Mensagem Pendente


	public enum RegistroSituacao
    {
        TpFisica = 1,
        TpJuridica = 2
    }
}
