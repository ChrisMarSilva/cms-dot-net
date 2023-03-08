namespace Catalogo.Domain.Models;

public class Categoria : BaseEntity
{
    //public Guid Id { get; set; }
    public string? Nome { get; set; }
    public string? ImagemUrl { get; set; }
}
