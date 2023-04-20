namespace Catalogo.Application.Dtos;

public class ProdutoRequestDTO
{
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public string ImagemUrl { get; set; } = string.Empty;
    public float Estoque { get; set; }
    public Guid CategoriaId { get; set; }
}
