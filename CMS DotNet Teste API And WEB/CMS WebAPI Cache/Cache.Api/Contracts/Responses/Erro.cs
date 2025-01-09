namespace Cache.Api.Contracts.Responses;

public class Erro
{
    public Erro(string campo, ErroResponseDto parent)
    {
        _parent = parent;
        Campo = campo;
        Mensagens = new List<string>();
    }

    private readonly ErroResponseDto _parent;
    public string Campo { get; private set; }
    public IList<string> Mensagens { get; private set; }

    public Erro AddMensagem(string mensagem)
    {
        Mensagens.Add(mensagem);
        return this;
    }

    public Erro ThenAddErro(string campo)
    {
        return _parent.AddErro(campo);
    }

    public ErroResponseDto Build()
    {
        return _parent;
    }
}
