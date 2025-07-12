using Microsoft.AspNetCore.Identity;

namespace CMS_WebAPI_OAuth.Models;

public class ApplicationUser : IdentityUser<Guid>
{
    private ApplicationUser() { }

    public ApplicationUser(string? firstName = null, string? lastName = null) : base()
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
    }

    public string? FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    //public string Token { get; private set; }
    public string RefreshToken { get; private set; }
    public DateTime? DtHrExpireRefreshToken { get; private set; }

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