using System.ComponentModel.DataAnnotations;

namespace Project.Domain.Dtos.Request;

public record ClientRequestDto
{
    [Required(ErrorMessage = "Campo obrigatório.", AllowEmptyStrings = false)]
    [StringLength(150, MinimumLength = 1, ErrorMessage = "Máximo de {1} caracteres.")]
    public string? Name { get; init; } = null!;

    public bool IsValid()
    {
        return true;
    }
}
