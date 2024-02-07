namespace Teste_com_Lista.Models;

public sealed class PagadorModel
{
    private PagadorModel()
    {
        PagadorContas = new HashSet<PagadorContaModel>();
    }

    internal PagadorModel(decimal numCtrlReq) : this()
    {
        NumCtrlReq = numCtrlReq;
    }

    /// <summary>
    /// Solicitação do Pagador pela Mensagem DDA0001/DDA0005
    /// </summary>
    public PagadorModel(decimal numCtrlReq, int ispbPartRecbdrPrincipal, int ispbPartRecbdrAdmtd,
        string tpPessoaPagdr, long cpfCnpjPagdr, ulong? numIdentcPagdr, ulong? numRefAtlCadCliPagdr,
        string indrAdesCliPagdrDda, int? sitCliPagdrDda, DateTime? dtHrSitAdesCliPagdrDda,
        DateTime? dtHrIniCadCliPagdrDda, DateTime? dtHrFimCadCliPagdrDda, int? sitCliPagdrPart,
        DateTime? dtHrSitCliPagdrPart, DateTime dtMovto, string numOp, Guid userId,
        string idempotenceKey)
        : this(numCtrlReq)
    {
        IspbPartRecbdrPrincipal = ispbPartRecbdrPrincipal;
        IspbPartRecbdrAdmtd = ispbPartRecbdrAdmtd;
        TpPessoaPagdr = tpPessoaPagdr;
        CpfCnpjPagdr = cpfCnpjPagdr;
        NumIdentcPagdr = numIdentcPagdr;
        NumRefAtlCadCliPagdr = numRefAtlCadCliPagdr;
        IndrAdesCliPagdrDda = indrAdesCliPagdrDda;
        SitCliPagdrDda = sitCliPagdrDda;
        DtHrSitAdesCliPagdrDda = dtHrSitAdesCliPagdrDda;
        DtHrIniCadCliPagdrDda = dtHrIniCadCliPagdrDda;
        DtHrFimCadCliPagdrDda = dtHrFimCadCliPagdrDda;
        SitCliPagdrPart = sitCliPagdrPart;
        DtHrSitCliPagdrPart = dtHrSitCliPagdrPart;
        DtMovto = dtMovto.Date;
        NumOp = numOp;
        UserId = userId;
        IdempotenceKey = idempotenceKey;
        DtHrRegistro = DateTime.Now;
    }
    public decimal NumCtrlReq { get; }
    public int IspbPartRecbdrPrincipal { get; }
    public int IspbPartRecbdrAdmtd { get; }
    public string TpPessoaPagdr { get; } = null!;
    public long CpfCnpjPagdr { get; }
    public ulong? NumIdentcPagdr { get; }
    public ulong? NumRefAtlCadCliPagdr { get; }
    public string IndrAdesCliPagdrDda { get; } = null!;
    public DateTime DtMovto { get; }
    public string NumOp { get; } = null!;
    public Guid? UserId { get; }
    public string? IdempotenceKey { get; }
    public int? SitCliPagdrDda { get; }
    public DateTime? DtHrSitAdesCliPagdrDda { get; }
    public DateTime? DtHrIniCadCliPagdrDda { get; }
    public DateTime? DtHrFimCadCliPagdrDda { get; }
    public int? SitCliPagdrPart { get; }
    public DateTime? DtHrSitCliPagdrPart { get; }
    public string? NomeArquivo { get; }
    public DateTime DtHrRegistro { get; }
    public ICollection<PagadorContaModel> PagadorContas { get; }

    public void AdicionarConta(string tpAgCliPagdr, int agCliPagdr, string tpCtCliPagdr, long ctCliPagdr, DateTime? dtAdesCliPagdrDda, string? indrManutCtCliPagdr)
    {
        PagadorContas.Add(new PagadorContaModel(NumCtrlReq, tpAgCliPagdr, agCliPagdr, tpCtCliPagdr, ctCliPagdr, dtAdesCliPagdrDda, indrManutCtCliPagdr));
    }
}