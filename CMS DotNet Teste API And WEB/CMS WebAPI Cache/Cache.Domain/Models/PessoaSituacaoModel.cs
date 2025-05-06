using Cache.Domain.Enums;
using Cache.Shared.Extensions;

namespace Cache.Domain.Models;

public sealed class PessoaSituacaoModel
{
    //For EF
    private PessoaSituacaoModel() { }

    public PessoaSituacaoModel(Guid idPessoa, SituacaoPessoa situacao, string? cdErro = null, string? txtErro = null) : this()
    {
        IdSituacao = Guid.NewGuid();
        IdPessoa = idPessoa;
        Situacao = situacao;
        CdErro = cdErro?.Truncate(100);
        TxtErro = txtErro?.Truncate(4000);
        DtHrRegistro = DateTime.UtcNow;
    }

    public Guid IdSituacao { get; }
    public Guid IdPessoa { get; }
    public SituacaoPessoa Situacao { get; }
    public string? CdErro { get; }
    public string? TxtErro { get; }
    public DateTime DtHrRegistro { get; }
    public PessoaModel? Pessoa { get; } = null;
}