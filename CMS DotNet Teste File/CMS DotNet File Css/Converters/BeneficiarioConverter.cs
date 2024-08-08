using CMS_DotNet_File_Css.Models;

namespace CMS_DotNet_File_Css.Converters;

public static class BeneficiarioConverter
{
    public static BeneficiarioModel CreateBeneficiarioModel(
        string cpfCnpjBeneficiario,
        string tpPessoaBeneficiario,
        string ispbIfAdministrada,
        string ispbIfPrincipal,
        string nmBeneficiario,
        string nmFantasia,
        string dtInicioRelacionamento,
        string dtFimRelacionamento,
        string sitRelacionamento,
        string sitBeneficiarioIf,
        string dthrSitBeneficiarioIf,
        string idBeneficiario,
        string numRefBeneficiario,
        string numSeqBeneficiario,
        string tpMsgArquivo,
        string numMsgRec,
        string idArqvRec,
        string dthrRegistro,
        string dthrAlteracao,
        string dtInicioRelacionamentoAntg,
        string dtInicioRelacionamentoRecte)
    {
        return new BeneficiarioModel
        {
            CPFCNPJ_BENEFICIARIO = cpfCnpjBeneficiario,
            TP_PESSOA_BENEFICIARIO = tpPessoaBeneficiario,
            ISPB_IF_ADMINISTRADA = ispbIfAdministrada,
            ISPB_IF_PRINCIPAL = string.IsNullOrEmpty(ispbIfPrincipal) ? (decimal?)null : decimal.Parse(ispbIfPrincipal),
            NM_BENEFICIARIO = nmBeneficiario,
            NM_FANTASIA = string.IsNullOrEmpty(nmFantasia) ? null : nmFantasia,
            DT_INICIO_RELACIONAMENTO = dtInicioRelacionamento,
            DT_FIM_RELACIONAMENTO = string.IsNullOrEmpty(dtFimRelacionamento) ? null : dtFimRelacionamento,
            SIT_RELACIONAMENTO = string.IsNullOrEmpty(sitRelacionamento) ? null : sitRelacionamento,
            SIT_BENEFICIARIO_IF = sitBeneficiarioIf,
            DTHR_SIT_BENEFICIARIO_IF = string.IsNullOrEmpty(dthrSitBeneficiarioIf) ? (decimal?)null : decimal.Parse(dthrSitBeneficiarioIf),
            ID_BENEFICIARIO = idBeneficiario,
            NUMREF_BENEFICIARIO = string.IsNullOrEmpty(numRefBeneficiario) ? (decimal?)null : decimal.Parse(numRefBeneficiario),
            NUMSEQ_BENEFICIARIO = string.IsNullOrEmpty(numSeqBeneficiario) ? (decimal?)null : decimal.Parse(numSeqBeneficiario),
            TP_MSG_ARQUIVO = string.IsNullOrEmpty(tpMsgArquivo) ? null : tpMsgArquivo,
            NUMMSG_REC = string.IsNullOrEmpty(numMsgRec) ? (decimal?)null : decimal.Parse(numMsgRec),
            ID_ARQV_REC = string.IsNullOrEmpty(idArqvRec) ? (decimal?)null : decimal.Parse(idArqvRec),
            DTHR_REGISTRO = string.IsNullOrEmpty(dthrRegistro) ? (decimal?)null : decimal.Parse(dthrRegistro),
            DTHR_ALTERACAO = string.IsNullOrEmpty(dthrAlteracao) ? (decimal?)null : decimal.Parse(dthrAlteracao),
            DT_INICIO_RELACIONAMENTO_ANTG = string.IsNullOrEmpty(dtInicioRelacionamentoAntg) ? null : dtInicioRelacionamentoAntg,
            DT_INICIO_RELACIONAMENTO_RECTE = string.IsNullOrEmpty(dtInicioRelacionamentoRecte) ? null : dtInicioRelacionamentoRecte
        };
    }
}