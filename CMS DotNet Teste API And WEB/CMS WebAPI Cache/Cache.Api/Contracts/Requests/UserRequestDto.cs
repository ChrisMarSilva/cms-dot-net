namespace Cache.Api.Contracts.Requests;

public sealed record UserRequestDto
{
    public string Name { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string Password { get; init; } = null!;

    public bool IsValid()
    {
        if (string.IsNullOrWhiteSpace(Name))
            return false;

        if (string.IsNullOrWhiteSpace(Email))
            return false;

        if (string.IsNullOrWhiteSpace(Password))
            return false;

        return true;
    }
};
