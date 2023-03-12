using System.Text.Json.Serialization;

namespace Catalogo.Domain.Models;

public class Produto : BaseEntity
{
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public decimal Preco { get; set; }
    public string? ImagemUrl { get; set; }
    public float Estoque { get; set; }
    public Guid CategoriaId { get; set; }

    //[JsonIgnore]
    public Categoria Categoria { get; set; }

    public Produto() : base()
    {

    }

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
}

