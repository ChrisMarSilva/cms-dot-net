namespace XGame.Domain.Arguments.Jogador;

public class AutenticarJogadorResponse
{
    public Guid Id { get; set; }
    public string PrimeiroNome { get; set; }
    public string Email { get; set; }
    public int Status { get; set; }
}
