using Microsoft.AspNetCore.Identity;

namespace Cache.Domain.Models.Auth;

public sealed class ApplicationUser : IdentityUser<Guid>
{
    private ApplicationUser() { }

    public ApplicationUser(string firstName = null, string lastName = null, DateTime? dtHrPasswordExpires = null) : base(userName)
    {
        Id = Guid.NewGuid();;
        FirstName = firstName;
        LastName = lastName;
        DtHrPasswordExpires = dtHrPasswordExpires ?? DateTime.UtcNow.AddDays(10);
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string RefreshToken { get; private set; }
    public DateTime? DtHrExpireRefreshToken { get; private set; }
    public DateTime DtHrPasswordExpires { get; private set; }

    public void Update(string firstName, string lastName, string telefone, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = telefone;
        Email = email;
    }

    internal void DefinirDataHoraExpiricaoSenha(DateTime dtHrPasswordExpires)
    {
        DtHrPasswordExpires = dtHrPasswordExpires;
    }

    public void SetRefreshToken(string refreshToken, DateTime expires)
    {
        RefreshToken = refreshToken;
        DtHrExpireRefreshToken = expires;
    }

    public void RevokeToken()
    {
        RefreshToken = null;
    }
}