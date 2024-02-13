namespace TesteBenchmarkDotNet.Models;

public sealed class ClienteCarteiraModel
{
    private ClienteCarteiraModel() { }

    internal ClienteCarteiraModel(short idCliente) : this()
    {
        IdCliente = idCliente;
    }

    public ClienteCarteiraModel(short idCliente, long saldo) : this(idCliente)
    {
        Saldo = saldo;
    }

    public short IdCliente { get; set; }
    public long Saldo { get; set; }
    public ClienteModel? Cliente { get; } = null;
}
