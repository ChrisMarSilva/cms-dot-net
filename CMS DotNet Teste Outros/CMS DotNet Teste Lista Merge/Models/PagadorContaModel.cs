using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Teste_com_Lista.Models;

public sealed class PagadorContaModel
{
    private PagadorContaModel() { }

    public PagadorContaModel(decimal numCtrlReq, string tpAgCliPagdr, int agCliPagdr,  string tpCtCliPagdr, long ctCliPagdr, DateTime? dtAdesCliPagdrDda, string? indrManutCtCliPagdr) : this()
    {
        NumCtrlReq = numCtrlReq;
        TpAgCliPagdr = tpAgCliPagdr;
        AgCliPagdr = agCliPagdr;
        TpCtCliPagdr = tpCtCliPagdr;
        CtCliPagdr = ctCliPagdr;
        DtAdesCliPagdrDda = dtAdesCliPagdrDda?.Date;
        IndrManutCtCliPagdr = indrManutCtCliPagdr;
    }

    public decimal NumCtrlReq { get; }
    public string TpAgCliPagdr { get; } = null!;
    public int AgCliPagdr { get; }
    public string TpCtCliPagdr { get; } = null!;
    public long CtCliPagdr { get; }
    public DateTime? DtAdesCliPagdrDda { get; }
    public string? IndrManutCtCliPagdr { get; }
    public PagadorModel? Pagador { get; } = null;

    public override string ToString()
    {
        return $"CONTA( NumCtrlReq: {NumCtrlReq}" +
            $" - TpAg: {TpAgCliPagdr}" +
            $" - Ag: {AgCliPagdr}" +
            $" - TpCt: {TpCtCliPagdr}" +
            $" - Ct: {CtCliPagdr}" +
            $" - DtAdes: {DtAdesCliPagdrDda:dd/MM/yyyy}" +
            $" - IndrManut: {IndrManutCtCliPagdr ?? "I"} )";
    }
}
