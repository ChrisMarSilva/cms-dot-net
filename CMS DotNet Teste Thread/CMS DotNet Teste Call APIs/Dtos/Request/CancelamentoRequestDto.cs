namespace CMS_DotNet_Teste_Call_APIs.Dtos.Request;

public class CancelamentoRequestDto
{
    public string IdRecorrencia { get; set; }
    public int TpPessoaSol { get; set; } // ex: 0 para pessoa física
    public string CpfCnpjSolCancelamento { get; set; } // sempre CPF neste cenário
    public string MotivoCancelamento { get; set; }
}