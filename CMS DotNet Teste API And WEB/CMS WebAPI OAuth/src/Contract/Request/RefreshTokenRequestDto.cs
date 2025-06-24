using System.ComponentModel.DataAnnotations;

namespace CMS_WebAPI_OAuth.Contract.Request;

public record RefreshTokenRequestDto
{
    [Required] public string AccessToken { get; init; } = null!;
    [Required] public string RefreshToken { get; init; } = null!;
}