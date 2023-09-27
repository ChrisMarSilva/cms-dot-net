namespace XGame.Domain.ValueObjects;

public class Nome
{
    protected Nome()  { }

    public Nome(string primeiroNome, string ultimoNome)
    {
        PrimeiroNome = primeiroNome;
        UltimoNome = ultimoNome;
    }

    public string PrimeiroNome { get; set; } = string.Empty;
    public string UltimoNome { get; set; } = string.Empty;
}
