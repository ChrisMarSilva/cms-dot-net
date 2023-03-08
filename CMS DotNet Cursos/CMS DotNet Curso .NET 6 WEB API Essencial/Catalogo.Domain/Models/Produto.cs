namespace Catalogo.Domain.Models;

public class Produto : BaseEntity
{
    //public Guid Id { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public decimal Preco { get; set; }
    public string? ImagemUrl { get; set; }
    public float Estoque { get; set; }
    // public DateTime DataCadastro { get; set; }
}

