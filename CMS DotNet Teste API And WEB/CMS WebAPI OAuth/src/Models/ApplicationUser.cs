using Microsoft.AspNetCore.Identity;
using TimeZoneConverter;

namespace CMS_WebAPI_OAuth.Models;

public class ApplicationUser : IdentityUser<Guid>
{
    private ApplicationUser() { }

    public ApplicationUser(string? firstName = null, string? lastName = null, DateTime? dtHrPasswordExpires = null) : base()
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        DtHrPasswordExpires = TimeZoneInfo.ConvertTime(
            dtHrPasswordExpires ?? DateTime.UtcNow.AddDays(10), 
            TZConvert.GetTimeZoneInfo("E. South America Standard Time"));
    }

    public string? FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public string RefreshToken { get; private set; }
    public DateTime DtHrPasswordExpires { get; private set; }
    public DateTime? DtHrExpireRefreshToken { get; private set; }

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