namespace Catalogo.Domain.Dtos;

public class AlunoRequestDTO
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int Idade { get; set; }
}
