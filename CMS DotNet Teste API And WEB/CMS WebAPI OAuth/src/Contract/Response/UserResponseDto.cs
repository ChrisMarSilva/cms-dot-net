namespace CMS_WebAPI_OAuth.Contract.Response;

public sealed record UserResponseDto
{
    public Guid UserId { get; init; }
    public string? UserName { get; init; } 
    public string? Email { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? Perfil { get; init; }
    public IList<string> Escopos { get; init; } = null!;
    public string? Telefone { get; init; }
}