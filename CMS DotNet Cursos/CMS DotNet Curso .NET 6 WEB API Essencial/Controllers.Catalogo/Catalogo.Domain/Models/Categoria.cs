using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Catalogo.Domain.Models;

public sealed class Categoria : BaseEntity
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(80, ErrorMessage = "O nome deve ter no máximo {1} e no mínimo {2} caracteres", MinimumLength = 5)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [StringLength(300, MinimumLength = 10)]
    public string ImagemUrl { get; set; } = string.Empty;
    public ICollection<Produto> Produtos { get; set; } // IEnumerable // ICollection 

    public Categoria() : base()
    {
        this.Produtos = new Collection<Produto>(); // List // Collection
    }

    public Categoria(string nome, string imagemUrl) : this()
    {
        Nome = nome;
        ImagemUrl = imagemUrl;
    }

    public Categoria(Guid id) : base(id)
    {
        this.Produtos = new Collection<Produto>(); // List // Collection
    }

    public Categoria(Guid id, string nome, string imagemUrl) : this(id)
    {
        Nome = nome;
        ImagemUrl = imagemUrl;
    }

    public void Update(string nome, string imagemUrl)
    {
        base.Update();

        Nome = nome;
        ImagemUrl = imagemUrl;
    }
}
