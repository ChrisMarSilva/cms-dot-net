using System.ComponentModel.DataAnnotations;

namespace CMS_WebAPI_OAuth.Contract.Request;

public record RoleUserRequestDto
{
    [Required] public string UserName { get; init; } = null!;
    [Required] public string Role { get; init; } = null!;
}