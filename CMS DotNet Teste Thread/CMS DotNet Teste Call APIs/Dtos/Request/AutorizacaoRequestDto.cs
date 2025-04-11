using CMS_DotNet_Teste_Call_APIs.Dtos.Enums;

namespace CMS_DotNet_Teste_Call_APIs.Dtos.Request;

public sealed record AutorizacaoRequestDto
{
    public string IdRecorrencia { get; init; } = null!;
    public TipoFrequencia TpFrequencia { get; init; }
    public DateTime DtInicialRecorrencia { get; init; }
    public DateTime? DtFinalRecorrencia { get; init; }
    public decimal? Valor { get; init; }
    public RecebedorDto Recebedor { get; init; } = null!;
    public PagadorDto Pagador { get; init; } = null!;
    public string NrContrato { get; init; } = null!;
    public string? DescContrato { get; init; }
    public DateTime DtHrCriacaoRecorrencia { get; init; }
    public DateTime DtHrCriacaoSolicitacao { get; init; }
    public DateTime DtHrExpiracaoSolicitacao { get; init; }
}