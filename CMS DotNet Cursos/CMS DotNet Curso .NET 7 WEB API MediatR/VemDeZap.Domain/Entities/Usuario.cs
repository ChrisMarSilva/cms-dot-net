using VemDeZap.Domain.Entities.Base;
using VemDeZap.Domain.Extensions;

namespace VemDeZap.Domain.Entities;

public class Usuario // : EntityBase
{
    protected Usuario()
    {

    }
    public Usuario(string primeiroNome, string ultimoNome, string email, string senha)
    {
        PrimeiroNome = primeiroNome;
        UltimoNome = ultimoNome;
        Email = email;
        Senha = senha;

        //new AddNotifications<Usuario>(this)
        //    .IfNullOrInvalidLength(x => x.PrimeiroNome, 3, 150, "O primeiro nome deve conter entre 3 a 150 caracteres")
        //    .IfNullOrInvalidLength(x => x.UltimoNome, 3, 150)
        //    .IfNotEmail(x => x.Email)
        //    .IfNullOrInvalidLength(x => x.Senha, 3, 32);

        //if (!string.IsNullOrEmpty(this.Senha))
        //    this.Senha = Senha.ConvertToMD5();

        //DataCadastro = DateTime.Now;
        //Ativo = false;
    }

    public Guid Id { get; set; }
    public string PrimeiroNome { get; set; } = string.Empty;
    public string UltimoNome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    //public DateTime DataCadastro { get; private set; }
    //public bool Ativo { get; private set; }
}
