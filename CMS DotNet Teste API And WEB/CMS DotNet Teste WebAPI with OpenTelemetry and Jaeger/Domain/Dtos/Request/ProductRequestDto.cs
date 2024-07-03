using System.ComponentModel.DataAnnotations;

namespace Project.Domain.Dtos.Request;

public record ProductRequestDto
{
    [Required(ErrorMessage = "Campo obrigatório.", AllowEmptyStrings = false)]
    [StringLength(150, MinimumLength = 1, ErrorMessage = "Máximo de {1} caracteres.")]
    public string? Name { get; init; } = null!;

    [Required(ErrorMessage = "Campo obrigatório.")]
    [Range(0.00, 999_999_999_999_999.99, ErrorMessage = "Intervalo aceito de {1} até {2}.")]
    public decimal? Price { get; init; }

    public bool IsValid()
    {
        var decimalValue = Price.GetValueOrDefault(0) - Math.Truncate(Price.GetValueOrDefault(0));
        if (decimalValue <= 0.00m)
            return false;

        return true;
    }
}
