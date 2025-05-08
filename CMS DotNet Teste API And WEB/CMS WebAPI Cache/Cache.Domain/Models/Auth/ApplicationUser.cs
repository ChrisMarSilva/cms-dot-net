using Microsoft.AspNetCore.Identity;

namespace Cache.Domain.Models.Auth;

public sealed class ApplicationUser : IdentityUser<Guid>
{
    private ApplicationUser() { }

    public ApplicationUser(int ispbPsp, bool ehSistema, string userName, string firstName = null, string lastName = null, bool solicAltSenha = false, DateTime? dtHrPasswordExpires = null) : base(userName)
    {
        Id = Guid.NewGuid();
        IspbPsp = ispbPsp;
        EhSistemaCliente = ehSistema;
        FirstName = firstName;
        LastName = lastName;
        SolicAltSenha = solicAltSenha;
        DtHrPasswordExpires = dtHrPasswordExpires ?? DateTime.UtcNow.AddDays(10);
    }

    public int IspbPsp { get; private set; }
    public bool EhSistemaCliente { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateTime? DtHrBloqueio { get; private set; }
    public string RefreshToken { get; private set; }
    public DateTime? DtHrExpireRefreshToken { get; private set; }
    public DateTime DtHrPasswordExpires { get; private set; }
    public bool SolicAltSenha { get; private set; }

    public void Update(string firstName, string lastName, string telefone, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = telefone;
        Email = email;
    }

    internal void SolicitarAlteracaoSenha(bool solicitar)
    {
        SolicAltSenha = solicitar;
    }

    internal void DefinirDataHoraExpiricaoSenha(DateTime dtHrPasswordExpires)
    {
        DtHrPasswordExpires = dtHrPasswordExpires;
    }

    internal void DefinirDataHoraBloqueio(DateTime? dataHoraBloqueio)
    {
        DtHrBloqueio = dataHoraBloqueio;
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