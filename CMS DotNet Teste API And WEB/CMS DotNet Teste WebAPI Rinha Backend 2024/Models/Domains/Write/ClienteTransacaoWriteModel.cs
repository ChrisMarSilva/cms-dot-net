namespace Rinha.Backend._2024.API.Models.Domains.Write;

internal sealed class ClienteTransacaoWriteModel
{
    public ClienteTransacaoWriteModel(short idCliente, long valor, string tipo, string descricao)
    {
        // IdTransacao = null;
        IdCliente = idCliente;
        IdCliente = idCliente;
        Valor = valor;
        Tipo = tipo;
        Descricao = descricao;
        DtHrRegistro = DateTime.Now;
    }

    public int IdTransacao { get; set; }
    public short IdCliente { get; set; }
    public long Valor { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime DtHrRegistro { get; private set; }
}
