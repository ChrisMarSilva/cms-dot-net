using System.Collections.ObjectModel;

namespace Catalogo.Domain.Models;

public class Categoria : BaseEntity
{
    public string Nome { get; set; } = string.Empty;
    public string ImagemUrl { get; set; } = string.Empty;
    public ICollection<Produto> Produtos { get; set; } // IEnumerable // ICollection 

    public Categoria() : base()
    {
        this.Produtos = new Collection<Produto>(); // List // Collection
    }

    public Categoria(string nome, string? imagemUrl) : this()
    {
        Nome = nome;
        ImagemUrl = imagemUrl;
    }

    public void Update(string nome, string? imagemUrl)
    {
        base.Update();

        Nome = nome;
        ImagemUrl = imagemUrl;
    }
}
