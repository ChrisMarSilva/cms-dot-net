namespace CMS_WebAPI_OAuth.Contract.Response;

public sealed record CreateUserResponseDto
{
    public string UserName { get; init; } = null!;
    public string Password { get; init; } = null!;
}