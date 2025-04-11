namespace CMS_DotNet_Teste_Call_APIs.Dtos;

public sealed record RecebedorDto
{
    public int Ispb { get; init; }
    public long Cnpj { get; init; }
    public string Nome { get; init; } = null!;
}