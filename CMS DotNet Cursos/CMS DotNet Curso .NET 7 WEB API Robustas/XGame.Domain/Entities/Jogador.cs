using prmToolkit.NotificationPattern;
using XGame.Domain.Enum;
using XGame.Domain.Extensions;
using XGame.Domain.ValueObjects;

namespace XGame.Domain.Entities;

public class Jogador : EntityBase
{
    public Jogador() { }

    public Jogador(Email email, string senha)
    {
        Email = email;
        Senha = senha;

        new AddNotifications<Jogador>(this).IfNullOrInvalidLength(x => x.Senha, 6, 32, "A senha deve ter entre 6 a 32 caracteres");

        if (IsValid())
            Senha = Senha.ConvertToMD5();
    }

    public Jogador(Nome nome, Email email, string senha)
    {
        Nome = nome;
        Email = email;
        Senha = senha;
        Status = EnumSituacaoJogador.EmAnalise;

        // new AddNotifications<Jogador>(this).IfNullOrInvalidLength(x => x.Senha, 6, 32, Message.X0_OBRIGATORIA_E_DEVE_CONTER_ENTRE_X1_E_X2_CARACTERES.ToFormat("Primeiro nome", "3", "50"));

        if (IsValid())
            Senha = Senha.ConvertToMD5();

        AddNotifications(nome, email);
    }

    public Nome Nome { get; private set; }
    public Email Email { get; private set; }
    public string Senha { get; private set; }
    public EnumSituacaoJogador Status { get; private set; }

    public void AlterarJogador(Nome nome, Email email, EnumSituacaoJogador status)
    {
        Nome = nome;
        Email = email;

        new AddNotifications<Jogador>(this).IfFalse(Status == EnumSituacaoJogador.Ativo, "Só é possível alterar jogador se ele estiver ativo.");

        AddNotifications(nome, email);
    }

    public override string ToString()
    {
        return this.Nome.PrimeiroNome + " " + this.Nome.UltimoNome;
    }
}