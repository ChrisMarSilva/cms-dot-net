using Rinha.Backend._2024.API.Converters;
using System.Text.Json.Serialization;

namespace Rinha.Backend._2024.API.Dtos;

public sealed record ExtratoResponseDto
{
    public ExtratoSaldoResponseDto? Saldo { get; set; } = null;
    public ICollection<ExtratoTransacoesResponseDto>? Transacoes { get; set; }
};

public sealed record ExtratoSaldoResponseDto
{
    public long Total { get; set; }
    [JsonConverter(typeof(DateTimeConverter))] public DateTime Data_Extrato { get; set; }
    public long Limite { get; set; }
};

public sealed record ExtratoTransacoesResponseDto
{
    public long Valor { get; set; }
    public string Tipo { get; set; } = null!;
    public string Descricao { get; set; } = null!;
    [JsonConverter(typeof(DateTimeConverter))] public DateTime Realizada_Em { get; set; }
};
