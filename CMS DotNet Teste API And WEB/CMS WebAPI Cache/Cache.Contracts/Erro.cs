using Cache.Contracts.Response;

namespace Cache.Contracts;

public class Erro
{
    public Erro(string campo, ErrorResponseDto parent)
    {
        _parent = parent;
        Campo = campo;
        Mensagens = new List<string>();
    }

    private readonly ErrorResponseDto _parent;
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

    public ErrorResponseDto Build()
    {
        return _parent;
    }
}
