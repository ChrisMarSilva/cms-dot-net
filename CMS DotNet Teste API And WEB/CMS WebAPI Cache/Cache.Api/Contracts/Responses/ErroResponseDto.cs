using System.Net;
using FluentValidation;
using FluentValidation.Results;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cache.Api.Contracts.Responses;

public class ErroResponseDto
{
    public ErroResponseDto(string codigo, string? mensagem = null)
    {
        Codigo = codigo;
        Mensagem = mensagem;
    }

    public ErroResponseDto(HttpStatusCode codigo, string? mensagem = null) 
        : this(((int)codigo).ToString(), mensagem) { }

    public ErroResponseDto(int codigo, string? mensagem = null)
        : this(codigo.ToString(), mensagem) {  }

    private IList<Erro> _erros;
    public string Codigo { get; }
    public string? Mensagem { get; }
    public IEnumerable<Erro> Erros => _erros;

    public static ErroResponseDto Iniciar(string codigo, string? mensagem = null) =>
        new ErroResponseDto(codigo, mensagem);

    public static ErroResponseDto Iniciar(int codigo, string? mensagem = null) =>
        new ErroResponseDto(codigo, mensagem);

    public static ErroResponseDto Iniciar(HttpStatusCode codigo, string? mensagem = null) =>
        new ErroResponseDto(codigo, mensagem);

    public static ErroResponseDto Iniciar(List<ValidationFailure>? errors = null)
    {
        var erroResponseDto = new ErroResponseDto(HttpStatusCode.BadRequest, "Payload inválido");

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
