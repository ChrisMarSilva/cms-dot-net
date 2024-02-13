using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

// dotnet run -c Release

//public class Program
//{
//    static void Main(string[] args)
//    {
BenchmarkRunner.Run<JsonComparisons>();
//    }
//}

public class Produto
{
    public Produto()
    {
    }

    public Produto(int id, string nome, string categoria, string descricao, double preco)
    {
        Id = id;
        Nome = nome;
        Categoria = categoria;
        Descricao = descricao;
        Preco = preco;
    }

    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public double Preco { get; set; }
}

[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(List<Produto>))]
public partial class ProdutoGenerationContext : JsonSerializerContext { }

[MemoryDiagnoser]
[RankColumn]
public class JsonComparisons
{
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly List<Produto> _produtos;

    [Params(10, 100, 1_000, 10_000, 100_000, 1_000_000)]
    public int TamanhoLista { get; set; }

    public JsonComparisons()
    {
        _jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        _produtos = ConstruirProdutos(TamanhoLista);
    }

    private List<Produto> ConstruirProdutos(int count)
    {
        // return new List<Produto>()
        //{
        //    new Produto() { Id = 1, Nome = "Name 1", Categoria = "Category 1", Descricao= "Description 1", Preco = 1 },
        //    new Produto() { Id = 2, Nome = "Name 2", Categoria = "Category 2", Descricao= "Description 2", Preco = 2 },
        //    new Produto() { Id = 3, Nome = "Name 3", Categoria = "Category 3", Descricao= "Description 3", Preco = 3 }
        //};

        //var faker = new Faker<Produto>()
        //    .CustomInstantiator(f => new Produto(
        //        id: f.UniqueIndex, 
        //        nome: f.Name.FullName(),
        //        categoria: f.Commerce.ProductName(),
        //        descricao: f.Commerce.ProductDescription(),
        //        preco: 100
        //    ));

        //return faker.Generate(count);

        return Enumerable.Range(1, count)
            .Select(i => new Produto()
            {
                Id = i,
                Nome = $"Nome {i}",
                Categoria = $"Categoria {i}",
                Descricao = $"Descrição {i}",
                Preco = i
            }).ToList();
    }

    [Benchmark]
    public string Newtonsoft() => JsonConvert.SerializeObject(_produtos);

    [Benchmark]
    public string SystemTextJson() => System.Text.Json.JsonSerializer.Serialize(_produtos, _jsonOptions);

    [Benchmark]
    public string SourceGenerator() => System.Text.Json.JsonSerializer.Serialize(_produtos, ProdutoGenerationContext.Default.ListProduto);
}


