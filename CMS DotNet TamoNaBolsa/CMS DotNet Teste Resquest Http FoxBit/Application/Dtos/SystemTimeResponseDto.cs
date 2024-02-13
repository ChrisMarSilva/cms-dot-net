namespace CMS_DotNet_Teste_Resquest_Http_FoxBit.Application.Dtos;

public class SystemTimeResponseDto
{
    public string iso { get; set; } = string.Empty;
    public long timestamp { get; set; }
}

//public class SystemTimeResponseDto
//{
//    [property: JsonPropertyName("id")] public Guid Id { get; set; }
//    [property: JsonPropertyName("nome")] public string Nome { get; set; } = string.Empty;
//    [property: JsonPropertyName("email")] public string Email { get; set; } = string.Empty;
//    [property: JsonPropertyName("idade")] public int Idade { get; set; }
//}