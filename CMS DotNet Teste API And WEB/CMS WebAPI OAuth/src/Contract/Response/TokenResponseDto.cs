namespace CMS_WebAPI_OAuth.Contract.Response;

public sealed record TokenResponseDto
{
    public long expires_in { get; init; }
    public string access_token { get; init; } = null!;
    public string refresh_token { get; init; } = null!;
    public string token_type { get; init; } = null!;
    public string scope { get; init; } = null!;
}