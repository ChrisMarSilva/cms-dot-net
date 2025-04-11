using CMS_DotNet_Teste_Call_APIs.Dtos.Enums;

namespace CMS_DotNet_Teste_Call_APIs.Dtos;

public sealed record PagadorDto
{
    public int Ispb { get; init; }
    public TipoOwner TpPessoa { get; init; }
    public long CpfCnpj { get; init; }
    public string? Nome { get; init; }
    public string? NrAgencia { get; init; }
    public TipoConta TpConta { get; init; }
    public string NrConta { get; init; } = null!;
    public int? CodMunIbge { get; init; } = null;
}