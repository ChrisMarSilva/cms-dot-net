using System.Text.Json.Serialization;

namespace Catalogo.Application.Dtos;

public class AlunoResponseDTO
{
    [property: JsonPropertyName("id")] public Guid Id { get; set; }
    [property: JsonPropertyName("nome")] public string Nome { get; set; } = string.Empty;
    [property: JsonPropertyName("email")] public string Email { get; set; } = string.Empty;
    [property: JsonPropertyName("idade")] public int Idade { get; set; }
}
