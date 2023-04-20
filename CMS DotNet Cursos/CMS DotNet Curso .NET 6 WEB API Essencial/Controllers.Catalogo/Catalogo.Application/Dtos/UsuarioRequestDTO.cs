using System.ComponentModel.DataAnnotations;

namespace Catalogo.Application.Dtos;

public class UsuarioRequestDTO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    [Display(Name = "Confirma senha")]
    [Compare("Password", ErrorMessage = "Senhas não conferem")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
