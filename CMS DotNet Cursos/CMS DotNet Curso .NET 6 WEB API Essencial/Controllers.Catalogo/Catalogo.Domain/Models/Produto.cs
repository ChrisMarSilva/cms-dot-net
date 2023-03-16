using System.ComponentModel.DataAnnotations;

namespace Catalogo.Domain.Models;

public class Produto : BaseEntity, IValidatableObject
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(80, ErrorMessage = "O nome deve ter no máximo {1} e no mínimo {2} caracteres", MinimumLength = 5)]
    //[PrimeiraLetraMaiuscula]
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public string ImagemUrl { get; set; } = string.Empty;
    public float Estoque { get; set; }
    public Guid CategoriaId { get; set; }
    //[JsonIgnore]
    public Categoria Categoria { get; set; }

    public Produto() : base() { }

    public Produto(string nome, decimal preco, float estoque, Guid categoriaId, string? descricao, string? imagemUrl) : this()
    {
        Nome = nome;
        Descricao = descricao;
        Preco = preco;
        ImagemUrl = imagemUrl;
        Estoque = estoque;
        CategoriaId = categoriaId;
    }

    public void Update(string nome, decimal preco, float estoque, Guid categoriaId, string? descricao, string? imagemUrl)
    {
        base.Update();

        Nome = nome;
        Descricao = descricao;
        Preco = preco;
        ImagemUrl = imagemUrl;
        Estoque = estoque;
        CategoriaId = categoriaId;
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!string.IsNullOrEmpty(this.Nome))
        {
            var primeiraLetra = this.Nome[0].ToString();
            if (primeiraLetra != primeiraLetra.ToUpper())
                yield return new ValidationResult("A primeira letra do produto deve ser maiúscula", new[] { nameof(this.Nome) } );
        }

        if (this.Estoque <= 0)
            yield return new ValidationResult("O estoque deve ser maior que zero", new[] { nameof(this.Estoque) } );
    }
}

