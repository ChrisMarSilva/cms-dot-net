using System.ComponentModel;

namespace CMS_DotNet_Teste_Call_APIs.Dtos.Enums;

public enum TipoFrequencia
{
    [Description("WEEK")]
    Semanal,
    [Description("MNTH")]
    Mensal,
    [Description("QURT")]
    Trimestral,
    [Description("MIAN")]
    Semestral,
    [Description("YEAR")]
    Anual
}