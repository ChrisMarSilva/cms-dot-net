namespace Rinha.Backend._2024.API.Dtos;

public sealed class ClienteTransacaoDto
{
    public int IdTransacao { get; set; }
    public int IdCliente { get; set; }
    public long Valor { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime DtHrRegistro { get; set; }
}
