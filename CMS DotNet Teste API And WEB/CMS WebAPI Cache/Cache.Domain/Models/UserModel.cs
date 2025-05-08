namespace Cache.Domain.Models;

public sealed class UserModel
{
    public UserModel()
    {
    }

    public UserModel(string name, string email, string password)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Password = password;
        DtHrCreated = DateTime.UtcNow;
    }

    public UserModel(Guid id, string name, string email, string password, DateTime dtHrCreated)
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
        DtHrCreated = dtHrCreated;
    }

    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public DateTime DtHrCreated { get; }

    public void EnsureIsValid()
    {
        if (string.IsNullOrEmpty(Name)) throw new Exception("Name is required.");
        if (string.IsNullOrEmpty(Email)) throw new Exception("Email is required.");
        if (string.IsNullOrEmpty(Password)) throw new Exception("Password is required.");
    }

    public void AlterUser(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
    }
}
