using prmToolkit.NotificationPattern.Resources;
using prmToolkit.NotificationPattern;

namespace XGame.Domain.ValueObjects;

public class Email : Notifiable
{
    protected Email() { }

    public Email(string endereco)
    {
        Endereco = endereco;

        // new AddNotifications<Email>(this).IfNotEmail(x => x.Endereco, Message.X0_INVALIDO.ToFormat("E-mail"));
    }

    public string Endereco { get; set; } = string.Empty;
}
