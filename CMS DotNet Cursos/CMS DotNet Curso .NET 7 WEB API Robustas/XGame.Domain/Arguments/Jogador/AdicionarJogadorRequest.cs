namespace XGame.Domain.Arguments.Jogador;

public class AdicionarJogadorRequest
{
    public string Email { get; set; }
    public string Senha { get; set; }
    public string PrimeiroNome { get; set; }
    public string UltimoNome { get; set; }
}
