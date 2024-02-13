//namespace Rinha.Backend._2024.API.Models;

//public sealed class ClienteTransacaoModel
//{
//    private ClienteTransacaoModel() { }

//    internal ClienteTransacaoModel(short idCliente) : this()
//    {
//        IdCliente = idCliente;
//    }

//    public ClienteTransacaoModel(short idCliente, long valor, string tipo, string descricao) : this()
//    {
//        // IdTransacao = null;
//        Valor = valor;
//        Tipo = tipo;
//        Descricao = descricao;
//        DtHrRegistro = DateTime.Now;
//    }

//    public int IdTransacao { get; set; }
//    public short IdCliente { get; set; }
//    public long Valor { get; set; }
//    public string Tipo { get; set; } = string.Empty;
//    public string Descricao { get; set; } = string.Empty;
//    public DateTime DtHrRegistro { get; private set; }
//    public ClienteModel? Cliente { get; } = null;
//}
