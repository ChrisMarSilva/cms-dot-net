//namespace Rinha.Backend._2024.API.Models;

//public sealed class ClienteModel
//{
//    private ClienteModel()
//    {
//        Transacoes = new HashSet<ClienteTransacaoModel>();
//    }

//    internal ClienteModel(short idCliente) : this()
//    {
//        IdCliente = idCliente;
//    }

//    public ClienteModel(short idCliente, long limite) : this(idCliente)
//    {
//        Limite = limite;
//    }

//    public short IdCliente { get; set; }
//    public long Limite { get; set; }
//    public ClienteCarteiraModel? Carteira { get; private set; }
//    public ICollection<ClienteTransacaoModel> Transacoes { get; }

//    //public void AdicionarCarteira(long saldo)
//    //{
//    //    Carteira = new ClienteCarteiraModel(IdCliente, saldo);
//    //}

//    //public void AdicionarTransacao(long valor, string tipo, string descricao)
//    //{
//    //    Transacoes.Add(new ClienteTransacaoModel(IdCliente, valor, tipo, descricao));
//    //}
//}