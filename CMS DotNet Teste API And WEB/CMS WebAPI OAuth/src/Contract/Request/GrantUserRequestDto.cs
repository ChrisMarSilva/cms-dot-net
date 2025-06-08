using System.ComponentModel.DataAnnotations;

namespace CMS_WebAPI_OAuth.Contract.Request;

public record GrantUserRequestDto
{
    [Required] public string UserName { get; init; } = null!;
    [Required] public string Scopes { get; init; } = null!;
}