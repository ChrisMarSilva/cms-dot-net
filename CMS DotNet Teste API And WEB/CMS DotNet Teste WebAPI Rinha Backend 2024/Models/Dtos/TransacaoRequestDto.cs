using System.ComponentModel.DataAnnotations;

namespace Rinha.Backend._2024.API.Models.Dtos;

public class TransacaoRequestDto
{
    [Required(ErrorMessage = "Campo obrigatório.")]
    //[RegularExpression("^\\d{1,9223372036854775808 }$", ErrorMessage = "Valor informado não confere com a expressão regular aceita {1}.")]
    public long? Valor { get; init; }

    [Required(ErrorMessage = "Campo obrigatório.")]
    [RegularExpression("^(c|d)$")]
    [StringLength(1, MinimumLength = 1, ErrorMessage = "Máximo de {1} caractere.")]
    public string? Tipo { get; init; } = null!;

    [Required(ErrorMessage = "Campo obrigatório.")]
    [StringLength(10, MinimumLength = 1, ErrorMessage = "Máximo de {1} caracteres.")]
    public string? Descricao { get; init; } = string.Empty;

    public bool IsValid()
    {

        /*
          if (request.Tipo!.Equals("d", StringComparison.OrdinalIgnoreCase))
                {
                    var novoSado = Math.Abs(saldoCarteira - request.Valor!.Value);
                    if (limiteCliente < novoSado) return Results.UnprocessableEntity("Novo saldo do cliente menor que seu limite disponível."); 

                    //carteira.Saldo -= request.Valor!.Value;
                }
                else if (request.Tipo!.Equals("C", StringComparison.OrdinalIgnoreCase))
                {
                    //carteira.Saldo += request.Valor!.Value;
                }
                else 
                {
                    return Results.UnprocessableEntity("Tipo de transação inválido."); 
                }

         */

        // errorResponseDto = ErrorResponseDto.Begin(HttpStatusCode.BadRequest, "Payload inválido.");

        //if (string.IsNullOrEmpty(Identificador))
        //{
        //    errorResponseDto.AddError("identificador").AddDescription("O campo \"identificador\" é obrigatório.");
        //}
        //else
        //{
        //    if (TipoConsulta is TipoConsultaDetalhePagador.NumIdentcPagdr)
        //    {
        //        if (Identificador!.Length > 19)
        //            errorResponseDto.AddError("identificador").AddDescription("O campo \"identificador\" só permite o máximo de 19 caracteres quando o campo \"tipoConsulta\" for \"1 - NumIdentcPagdr\".");

        //        // ulong ulongValue;
        //        if (!ulong.TryParse(Identificador, out ulong ulongValue))
        //            errorResponseDto.AddError("identificador").AddDescription("O campo \"identificador\" não contém um valor válido.");
        //    }
        //    else if (TipoConsulta is TipoConsultaDetalhePagador.NumCtrlReq)
        //    {
        //        if (Identificador!.Length > 20)
        //            errorResponseDto.AddError("identificador").AddDescription("O campo \"identificador\" só permite o máximo de 20 caracteres quando o campo \"tipoConsulta\" for \"3 - NumCtrlReq\".");

        //        // decimal decimalValue;
        //        if (!decimal.TryParse(Identificador, out decimal decimalValue))
        //            errorResponseDto.AddError("identificador").AddDescription("O campo \"identificador\" não contém um valor válido.");
        //    }
        //}

        //errorResponseDto.End();

        //return !(errorResponseDto.Errors?.Any() ?? false);

        return true;
    }
}
