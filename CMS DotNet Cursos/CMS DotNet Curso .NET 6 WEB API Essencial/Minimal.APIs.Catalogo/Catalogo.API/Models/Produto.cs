using System.Text.Json.Serialization;

namespace Catalogo.API.Models;

public class Produto
{
    public int ProdutoId { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public string Imagem { get; set; } = string.Empty;
    public DateTime DataCompra { get; set; }
    public int Estoque { get; set; }

    public int CategoriaId { get; set; }
    [JsonIgnore]
    public Categoria Categoria { get; set; }


    public Produto() { }

    public Produto(string nome, string descricao, decimal preco, string imagem, int estoque, int categoriaId)
    {
        this.Nome = nome;
        this.Descricao = descricao;
        this.Preco = preco;
        this.Imagem = imagem;
        this.DataCompra = DateTime.Now;
        this.Estoque = estoque;
        this.CategoriaId = categoriaId;
    }

    public Produto(int produtoId, string nome, string descricao, decimal preco, string imagem, int estoque, int categoriaId) : 
        this(nome, descricao, preco, imagem, estoque, categoriaId)
    {
        this.ProdutoId = produtoId;
    }
}
