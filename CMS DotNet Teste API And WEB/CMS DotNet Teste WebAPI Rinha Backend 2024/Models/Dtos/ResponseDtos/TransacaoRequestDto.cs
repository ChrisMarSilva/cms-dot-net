using System.ComponentModel.DataAnnotations;

namespace Rinha.Backend._2024.API.Models.Dtos.ResponseDtos;

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

    internal bool Valido()
    {
        if (!Valor.HasValue || Valor.Value < 1 || Valor.Value > 9223372036854775807) return false;
        if (string.IsNullOrEmpty(Tipo) || Tipo.Length != 1) return false;
        if (string.IsNullOrEmpty(Descricao) || Descricao.Length > 10) return false;
        if (!Tipo.Equals("d", StringComparison.OrdinalIgnoreCase) && !Tipo.Equals("c", StringComparison.OrdinalIgnoreCase)) return false;
        return true;
    }
}
