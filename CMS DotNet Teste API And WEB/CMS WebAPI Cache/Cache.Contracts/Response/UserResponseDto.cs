namespace Cache.Contracts.Response;

public sealed record UserResponseDto
{
    public UserResponseDto(Guid id, string name, string email, string password, DateTime dtHrCreated)
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
        DtHrCreated = dtHrCreated;
    }

    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string Password { get; init; } = null!;
    public DateTime DtHrCreated { get; init; }
};