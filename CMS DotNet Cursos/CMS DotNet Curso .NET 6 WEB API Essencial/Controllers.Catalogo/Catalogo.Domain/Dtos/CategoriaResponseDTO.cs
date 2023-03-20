namespace Catalogo.Domain.Dtos;

public class CategoriaResponseDTO
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string ImagemUrl { get; set; } = string.Empty;
    //public ICollection<ProdutoDTO> Produtos { get; set; }
}
