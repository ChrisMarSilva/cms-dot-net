using Bogus;
using CMS_DotNet_Teste_WebAPI_with_Redis.Models;

namespace CMS_DotNet_Teste_WebAPI_with_Redis.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private List<Produto> _produtos { get; set; } //private readonly IEnumerable<Produto> _produtos;

    public ProdutoRepository()
    {
        // _produtos = new List<Produto>();
        //var _produtos = new List<Produto> {  new Produto("Smith"), new Produto("Wilson"), new Produto("Berg"), new Produto("Wilson") };

        var fakeProduts = new Faker<Produto>("pt_BR")
               .RuleFor(o => o.Id, f => f.Random.Guid()) // Guid.NewGuid()
               .RuleFor(c => c.Nome, f => f.Commerce.Product());

        _produtos = fakeProduts.Generate(3);
    }

    public async Task<List<Produto>> GetAllAsync()
    {
        await Task.Delay(1);

        return _produtos.ToList(); // ToArray
    }

    public async Task<Produto> GetByIdAsync(Guid id)
    {
        await Task.Delay(1);

        var result = _produtos
            .FirstOrDefault(p => p.Id.Equals(id))
            ?? new Produto();

        return result;

        //foreach (var prod in _produtos)
        //    if (prod.Id.Equals(id))
        //        return prod;

        //return new Produto();
    }

    public async Task<Produto> CreateAsync(Produto input)
    {
        await Task.Delay(1);

        _produtos.Add(input);

        return input;
    }

    public Produto Update(Produto input)
    {
        for (int i = 0; i < _produtos.Count; i++) // foreach (var prod in _produtos)
        {
            if (_produtos[i].Id.Equals(input.Id))
            {
                _produtos[i].Nome = input.Nome;
                break;
            }
        }

        return input;
    }

    public bool Delete(Produto input)
    {
        var itemToRemove = _produtos
            .SingleOrDefault(p => p.Id.Equals(input.Id));

        if (itemToRemove != null)
        {
            _produtos.Remove(itemToRemove);
            return true;
        }

        //for (int i = 0; i < _produtos.Count; i++)
        //{
        //    if (_produtos[i].Id.Equals(input.Id))
        //    {
        //        _produtos.RemoveAt(i);
        //        break;
        //    }
        //}

        return false;
    }
}
