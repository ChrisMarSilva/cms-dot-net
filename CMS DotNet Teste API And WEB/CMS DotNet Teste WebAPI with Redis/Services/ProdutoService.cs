using CMS_DotNet_Teste_WebAPI_with_Redis.Dtos;
using CMS_DotNet_Teste_WebAPI_with_Redis.Models;
using CMS_DotNet_Teste_WebAPI_with_Redis.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace CMS_DotNet_Teste_WebAPI_with_Redis.Services;

public class ProdutoService : IProdutoService
{
    private readonly ILogger<ProdutoService> _logger;
    private readonly IProdutoRepository _prodRepo;
    private readonly IDistributedCache _cache; //  private readonly IMemoryCache _memoryCache;

    public ProdutoService(
        ILogger<ProdutoService> logger,
        IProdutoRepository produtoRepo,
        IDistributedCache cache // IMemoryCache memoryCache
        )
    {
        _logger = logger;
        _prodRepo = produtoRepo;
        _cache = cache; //  _memoryCache = memoryCache;
    }

    public async Task<IEnumerable<ProdutoResponseDto>> GetAllAsync()
    {
        try
        {
            var produtos = new List<Produto>();

            var cacheKey = "Products";
            var json = await _cache.GetStringAsync(cacheKey); // var json = _memoryCache.Get<Produto>(cacheKey);
            //var json = await _cache.GetRecordAsync<List<Produto>>(cacheKey);

            if (json != null)
            {
                _logger.LogTrace("Cache hit for {CacheKey}", cacheKey);
                produtos = JsonSerializer.Deserialize<List<Produto>>(json);
            }
            else
            {
                produtos = await _prodRepo.GetAllAsync();
                _logger.LogTrace("Cache hit for {CacheKey}", cacheKey);

                json = JsonSerializer.Serialize<List<Produto>>(produtos);

                var options = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(20))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(1));

                await _cache.SetStringAsync(cacheKey, json, options); //  _memoryCache.Set(cacheKey, json, TimeSpan.FromMinutes(1));
                //await _cache.SetRecordAsync<List<Produto>>(cacheKey, produtos);
                
                _logger.LogTrace("Setting items in cache for {CacheKey}", cacheKey);
            }

            var results = produtos?
                .Select(x => new ProdutoResponseDto { Id = x.Id, Nome = x.Nome })
                .ToList();

            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro: {ex.Message}");
            throw;
        }
    }

    public async Task<ProdutoResponseDto> GetByIdAsync(Guid id)
    {
        try
        {
            var produto = new Produto();

            var cacheKey = $"Product:{id}";
            var json = await _cache.GetStringAsync(cacheKey); // var json = _memoryCache.Get<Produto>(cacheKey);

            if (json != null)
            {
                _logger.LogTrace("Cache hit for {CacheKey}", cacheKey);
                produto = JsonSerializer.Deserialize<Produto>(json);
            }
            else
            {
                produto = await _prodRepo.GetByIdAsync(id);
                _logger.LogTrace("Cache hit for {CacheKey}", cacheKey);

                json = JsonSerializer.Serialize<Produto>(produto);

                var options = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(20))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(1));

                _logger.LogTrace("Setting items in cache for {CacheKey}", cacheKey);
                await _cache.SetStringAsync(cacheKey, json, options); //  _memoryCache.Set(cacheKey, json, TimeSpan.FromMinutes(1));
            }

            if (produto == null || produto == default(Produto))
                return null;

            var result = new ProdutoResponseDto
            {
                Id = produto.Id,
                Nome = produto.Nome
            };

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro: {ex.Message}");
            throw;
        }
    }

    public async Task<ProdutoResponseDto> InsertAsync(ProdutoRequestDto input)
    {
        try
        {
            var prod = new Produto(nome: input.Nome);
            var produto = await _prodRepo.CreateAsync(prod);

            if (produto is null || produto?.Id == Guid.Empty)
                return null; // new Produto();

            var result = new ProdutoResponseDto { Id = produto.Id, Nome = produto.Nome };

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro: {ex.Message}");
            throw;
        }
    }

    public async Task<ProdutoResponseDto> UpdateAsync(Guid id, ProdutoRequestDto input)
    {
        try
        {
            var prod = await _prodRepo.GetByIdAsync(id);

            if (prod == null || prod?.Id == Guid.Empty)
                return null; // new Produto();

            prod.Update(nome: input.Nome);

            var produto = _prodRepo.Update(prod);

            if (produto is null || produto?.Id == Guid.Empty)
                return null; // new Produto();

            var result = new ProdutoResponseDto { Id = produto.Id, Nome = produto.Nome };

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro: {ex.Message}");
            throw;
        }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        try
        {
            var prod = await _prodRepo.GetByIdAsync(id);

            if (prod is null || prod?.Id == Guid.Empty)
                return false;

            return _prodRepo.Delete(prod);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro: {ex.Message}");
            throw;
        }
    }
}
