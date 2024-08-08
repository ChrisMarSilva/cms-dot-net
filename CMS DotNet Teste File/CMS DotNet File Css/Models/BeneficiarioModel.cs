namespace CMS_DotNet_File_Css.Models;

public sealed class BeneficiarioModel
{
    public string CPFCNPJ_BENEFICIARIO { get; set; } = null!;
    public string TP_PESSOA_BENEFICIARIO { get; set; } = null!;
    public string ISPB_IF_ADMINISTRADA { get; set; } = null!;
    public decimal? ISPB_IF_PRINCIPAL { get; set; }
    public string NM_BENEFICIARIO { get; set; } = null!;
    public string? NM_FANTASIA { get; set; }
    public string DT_INICIO_RELACIONAMENTO { get; set; } = null!;
    public string? DT_FIM_RELACIONAMENTO { get; set; }
    public string? SIT_RELACIONAMENTO { get; set; }
    public string SIT_BENEFICIARIO_IF { get; set; } = null!;
    public decimal? DTHR_SIT_BENEFICIARIO_IF { get; set; }
    public string ID_BENEFICIARIO { get; set; } = null!;
    public decimal? NUMREF_BENEFICIARIO { get; set; }
    public decimal? NUMSEQ_BENEFICIARIO { get; set; }
    public string? TP_MSG_ARQUIVO { get; set; }
    public decimal? NUMMSG_REC { get; set; }
    public decimal? ID_ARQV_REC { get; set; }
    public decimal? DTHR_REGISTRO { get; set; }
    public decimal? DTHR_ALTERACAO { get; set; }
    public string? DT_INICIO_RELACIONAMENTO_ANTG { get; set; }
    public string? DT_INICIO_RELACIONAMENTO_RECTE { get; set; }
}
