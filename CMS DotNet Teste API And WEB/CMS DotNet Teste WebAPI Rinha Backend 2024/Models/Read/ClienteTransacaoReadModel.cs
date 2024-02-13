namespace Rinha.Backend._2024.API.Models.Read;

internal sealed class ClienteTransacaoReadModel
{
    public int IdTransacao { get; set; }
    public short IdCliente { get; set; }
    public long Valor { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime DtHrRegistro { get; set; }
}
