namespace XGame.Domain.ValueObjects;

public class Email
{
    protected Email() { }

    public Email(string endereco)
    {
        Endereco = endereco;
    }

    public string Endereco { get; set; } = string.Empty;
}
