namespace Rinha.Backend._2024.API.Dtos;

public sealed record TransacaoResponseDto
{
    public long Limite { get; set; }
    public long Saldo { get; set; }
};
