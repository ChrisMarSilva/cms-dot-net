using FluentValidation.Results;
using System.Net;

namespace Cache.Contracts.Response;

public class ErrorResponseDto
{
    public ErrorResponseDto(string codigo, string? mensagem = null)
    {
        Codigo = codigo;
        Mensagem = mensagem;
    }

    public ErrorResponseDto(HttpStatusCode codigo, string? mensagem = null)
        : this(((int)codigo).ToString(), mensagem) { }

    public ErrorResponseDto(int codigo, string? mensagem = null)
        : this(codigo.ToString(), mensagem) { }

    private IList<Erro> _erros;
    public string Codigo { get; }
    public string? Mensagem { get; }
    public IEnumerable<Erro> Erros => _erros;

    public static ErrorResponseDto Iniciar(string codigo, string? mensagem = null) =>
        new ErrorResponseDto(codigo, mensagem);

    public static ErrorResponseDto Iniciar(int codigo, string? mensagem = null) =>
        new ErrorResponseDto(codigo, mensagem);

    public static ErrorResponseDto Iniciar(HttpStatusCode codigo, string? mensagem = null) =>
        new ErrorResponseDto(codigo, mensagem);

    public static ErrorResponseDto Iniciar(List<ValidationFailure>? errors = null)
    {
        var erroResponseDto = new ErrorResponseDto(HttpStatusCode.BadRequest, "Payload inválido");

        if (errors != null)
        {
            var errorsAgrupados = errors
                //.Where(e => e.PropertyName == error.PropertyName)
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToList()
            //.ToDictionary(g => string.Join("; ", g.Select(e => e.ErrorMessage))
            );

            foreach (var errorAgrupadoKey in errorsAgrupados)
            {
                var erroLocal = erroResponseDto.AddErro(errorAgrupadoKey.Key);
                //erroLocal.AddMensagem(string.Join("; ", errorAgrupado.Value));

                foreach (var errorAgrupadoValue in errorAgrupadoKey.Value)
                {
                    erroLocal.AddMensagem(errorAgrupadoValue);
                }
            }
        }

        return erroResponseDto;
    }

    public Erro AddErro(string campo)
    {
        if (_erros == null)
            _erros = new List<Erro>();

        var erro = new Erro(campo, this);
        _erros.Add(erro);

        return erro;
    }
}
