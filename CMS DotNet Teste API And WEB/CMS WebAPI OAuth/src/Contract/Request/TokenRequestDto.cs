using System.ComponentModel.DataAnnotations;

namespace CMS_WebAPI_OAuth.Contract.Request;

public record TokenRequestDto
{
    [Required] public string client_id { get; init; } = null!;
    [Required] public string client_secret { get; init; } = null!;
    [Required] public string grant_type { get; init; } = null!;
    [Required] public string scope { get; init; } = null!;
}