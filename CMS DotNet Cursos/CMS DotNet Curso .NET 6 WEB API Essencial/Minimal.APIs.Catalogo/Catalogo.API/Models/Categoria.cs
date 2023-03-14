using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Catalogo.API.Models;

public class Categoria
{
    public int CategoriaId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;

    [JsonIgnore]
    public ICollection<Produto> Produtos { get; set; }

    public Categoria() { }

    public Categoria(string nome, string descricao)
    {
        this.Nome = nome;
        this.Descricao = descricao;
    }

    public Categoria(int categoriaId, string nome, string descricao) : 
        this(nome, descricao)
    {
        this.CategoriaId = categoriaId;
    }
}
