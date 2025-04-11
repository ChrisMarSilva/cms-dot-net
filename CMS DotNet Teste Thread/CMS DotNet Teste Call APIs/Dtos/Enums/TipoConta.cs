using System.ComponentModel;

namespace CMS_DotNet_Teste_Call_APIs.Dtos.Enums;

public enum TipoConta
{
    [Description("Conta Corrente")]
    CACC,
    [Description("Conta-Salário")]
    SLRY,
    [Description("Conta de Poupança")]
    SVGS,
    [Description("Conta de Pagamento")]
    TRAN
}