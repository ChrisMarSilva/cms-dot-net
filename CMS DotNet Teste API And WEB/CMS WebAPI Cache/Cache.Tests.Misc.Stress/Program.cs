Console.WriteLine(new string('-', 60));
Console.WriteLine($"[{DateTime.Now.TimeOfDay}]: INICIO");
Console.WriteLine(new string('-', 60));
try
{

    var cobrancas = new List<CobrancaModel>
    {
        new CobrancaModel
        {
            Fluxo = FluxoCobranca.Enviados,
            EndToEndId = "E05442029202505071500dPP4I7nptuB",
            IdConciliacaoRecebedor = "GMsK7mi4ngt2Mc7eCCKgphXJ1Xblqlm",
            IdRecorrencia = "RR0544202920250507pqegxfk3dxg",
            Finalidade = FinalidadeAgendamento.Agendamento,
            DtVencimento = DateTime.Parse("2025-05-09"),
            Valor = 100.00m,
            MotivoRejeicao = null,
            Cancelado = false,
            DtHrRegistro = DateTime.Parse("2025-05-07 22:51:21.2281263")
        },
        new CobrancaModel
        {
            Fluxo = FluxoCobranca.Enviados,
            EndToEndId = "E05442029202505101500hDjK4fIv3Cf",
            IdConciliacaoRecebedor = "yQ6PDbXeGzEt75QLoF7slIGoEIU",
            IdRecorrencia = "RR0544202920250507pqegxfk3dxg",
            Finalidade = FinalidadeAgendamento.Agendamento,
            DtVencimento = DateTime.Parse("2025-05-10"),
            Valor = 200.00m,
            MotivoRejeicao = null,
            Cancelado = false,
            DtHrRegistro = DateTime.Parse("2025-05-07 22:52:24.9962105")
        },
        new CobrancaModel
        {
            Fluxo = FluxoCobranca.Enviados,
            EndToEndId = "E054420292025051015001ndAJZ6V7Ez",
            IdConciliacaoRecebedor = "H8PPCBUmPhF95HuuIFTADkyWcf2Ta",
            IdRecorrencia = "RR0544202920250507pqegxfk3dxg",
            Finalidade = FinalidadeAgendamento.Agendamento,
            DtVencimento = DateTime.Parse("2025-05-11"),
            Valor = 300.00m,
            MotivoRejeicao = null,
            Cancelado = false,
            DtHrRegistro = DateTime.Parse("2025-05-07 22:53:04.0197132")
        },
        new CobrancaModel
        {
            Fluxo = FluxoCobranca.Enviados,
            EndToEndId = "E054420292025051015001HrRX9NVtow",
            IdConciliacaoRecebedor = "AqA1NaiBnDGbPl9TFumkxDGkqdCax909",
            IdRecorrencia = "RR0544202920250507pqegxfk3dxg",
            Finalidade = FinalidadeAgendamento.Agendamento,
            DtVencimento = DateTime.Parse("2025-05-12"),
            Valor = 400.00m,
            MotivoRejeicao = null,
            Cancelado = false,
            DtHrRegistro = DateTime.Parse("2025-05-07 22:53:12.0167479")
        },
        new CobrancaModel
        {
            Fluxo = FluxoCobranca.Enviados,
            EndToEndId = "E05442029202505101500hfuDEYt6jfC",
            IdConciliacaoRecebedor = "a0ueoijjGzCQobL3FoOsV78DQu",
            IdRecorrencia = "RR0544202920250507pqegxfk3dxg",
            Finalidade = FinalidadeAgendamento.Agendamento,
            DtVencimento = DateTime.Parse("2025-05-15"), // DateTime.Parse("2025-05-13"),
            Valor = 500.00m,
            MotivoRejeicao = null,
            Cancelado = true,
            DtHrRegistro = DateTime.Parse("2025-05-07 22:53:19.0217624")
        },
        new CobrancaModel
        {
            Fluxo = FluxoCobranca.Enviados,
            EndToEndId = "E05442029202505101500CejR1CfJXSE",
            IdConciliacaoRecebedor = "52cx3ExVKB2ns4A1D1VFlnjO309",
            IdRecorrencia = "RR0544202920250507pqegxfk3dxg",
            Finalidade = FinalidadeAgendamento.Agendamento,
            DtVencimento = DateTime.Parse("2025-05-14"),
            Valor = 600.00m,
            MotivoRejeicao = null,
            Cancelado = false,
            DtHrRegistro = DateTime.Parse("2025-05-07 22:53:47.0501190")
        },
        new CobrancaModel
        {
            Fluxo = FluxoCobranca.Enviados,
            EndToEndId = "E05442029202505091500UD496cLKHpN",
            IdConciliacaoRecebedor = "GMsK7mi4ngt2Mc7eCCKgphXJ1Xblqlm",
            IdRecorrencia = "RR0544202920250507pqegxfk3dxg",
            Finalidade = FinalidadeAgendamento.TentativaPosVencimento,
            DtVencimento = DateTime.Parse("2025-05-09"),
            Valor = 700.00m,
            MotivoRejeicao = null,
            Cancelado = false,
            DtHrRegistro = DateTime.Parse("2025-05-09 20:36:51.0972832")
        },
        new CobrancaModel
        {
            Fluxo = FluxoCobranca.Enviados,
            EndToEndId = "E05442029202505091500loq9QiPbKU7",
            IdConciliacaoRecebedor = "GMsK7mi4ngt2Mc7eCCKgphXJ1Xblqlm",
            IdRecorrencia = "RR0544202920250507pqegxfk3dxg",
            Finalidade = FinalidadeAgendamento.ReenvioErroLiquidacao,
            DtVencimento = DateTime.Parse("2025-05-09"),
            Valor = 800.00m,
            MotivoRejeicao = null,
            Cancelado = false,
            DtHrRegistro = DateTime.Parse("2025-05-09 20:37:30.9537736")
        },
    };

    var primeiraTentativa = cobrancas
        .Where(x => x.Finalidade is FinalidadeAgendamento.Agendamento &&
                    x.Cancelado == false &&
                    x.MotivoRejeicao == null)
        .MaxBy(x => x.DtVencimento);

    if (primeiraTentativa is null)
        Console.WriteLine("ERRO: primeiraTentativa is null");

    // if (primeiraTentativa.IdConciliacaoRecebedor != cobrancaModel.IdConciliacaoRecebedor)

    Console.WriteLine("ok");
}
catch (Exception ex)
{
    Console.WriteLine(new string('-', 60));
    Console.WriteLine($"[{DateTime.Now.TimeOfDay}]: ERRO -> {ex.Message}");
    Console.WriteLine(new string('-', 60));
    throw;
}
finally
{
    Console.WriteLine(new string('-', 60));
    Console.WriteLine($"[{DateTime.Now.TimeOfDay}]: FIM");
    Console.WriteLine(new string('-', 60));
    Console.ReadKey();
}


public enum FluxoCobranca
{
    Enviados = 0, // PSP Recebedor
    Recebidos = 1 // PSP Pagador
}

public enum FinalidadeAgendamento
{
    Agendamento = 0,
    TentativaPosVencimento = 1,
    ReenvioErroLiquidacao = 2
}

public enum TipoConta
{
    CACC,
    SLRY,
    SVGS,
    TRAN
}

public sealed class CobrancaModel
{
    public FluxoCobranca Fluxo { get; set; }
    public string EndToEndId { get; set; } = null!;
    public string IdConciliacaoRecebedor { get; set; } = null!;
    public string IdRecorrencia { get; set; } = null!;
    public FinalidadeAgendamento Finalidade { get; set; }
    public DateTime DtVencimento { get; set; }
    public decimal Valor { get; set; }
    public string? MotivoRejeicao { get; set; }
    public bool Cancelado { get; set; }
    public DateTime DtHrRegistro { get; set; }
}