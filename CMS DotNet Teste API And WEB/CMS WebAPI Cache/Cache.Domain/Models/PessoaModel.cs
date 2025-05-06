using Cache.Domain.Enums;

namespace Cache.Domain.Models;

public sealed class PessoaModel
{
    //For EF
    private PessoaModel()
    {
        Situacoes = new HashSet<PessoaSituacaoModel>();
    }

    public PessoaModel(Guid id, string nome) : this()
    {
        Id = id;
        Nome = nome;
        DtHrRegistro = DateTime.UtcNow;

        NovaSituacao(SituacaoPessoa.EmProcessamento);
    }

    public Guid Id { get; }
    public string Nome { get; } = null!;
    public DateTime DtHrRegistro { get; }
    public ICollection<PessoaSituacaoModel> Situacoes { get; }

    private PessoaSituacaoModel NovaSituacao(SituacaoPessoa situacao, string? cdErro = null, string? txtErro = null)
    {
        var novaSituacao = new PessoaSituacaoModel(Id, situacao, cdErro, txtErro);
        Situacoes.Add(novaSituacao);

        return novaSituacao;
    }

    public void Pendente() =>
        NovaSituacao(SituacaoPessoa.Pendente);

    public void Rejeitada(string cdErro, string? txtErro) => 
        NovaSituacao(SituacaoPessoa.Rejeitado, cdErro, txtErro);

    public void Aceita() => 
        NovaSituacao(SituacaoPessoa.Aceita);
}
