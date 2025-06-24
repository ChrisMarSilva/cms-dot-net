using System.ComponentModel.DataAnnotations;

namespace CMS_WebAPI_OAuth.Contract.Request;

public record CreateUserRequestDto
{
    [Required] public string UserName { get; init; } = null!;
    [Required] public string FirstName { get; init; } = null!;
    public string? LastName { get; init; }
    [Required] public string Password { get; init; } = null!;
    [Required] public string Email { get; init; } = null!;
    public string Telefone { get; init; } = null!;
}