using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.Caching.Distributed;

namespace WebApi.Caching.Redis.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TimerController : ControllerBase
{
    private readonly ILogger<TimerController> _logger;
    private readonly IDistributedCache _cache;

    public TimerController(ILogger<TimerController> logger, 
        IDistributedCache cache )
    {
        _logger = logger;
        _cache = cache;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        await Task.Delay(1);
        return Ok(DateTime.Now);
    }

    [HttpGet("teste")]
    [OutputCache(PolicyName = "Expire30")]
    public async Task<IActionResult> Get2()
    {
        await Task.Delay(1);
        return Ok(DateTime.Now);
    }

    [HttpPost("setvalue")]
    public async Task<IActionResult> SetValue()
    {
        var cacheKey = "dados2";
        var cacheDados = "Dados importantes com valor";

        var cacheOptions = new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) };
        await _cache.SetStringAsync(cacheKey, cacheDados, cacheOptions);

        return Ok();
    }

    [HttpGet("getvalue")]
    public async Task<IActionResult> GetValue()
    {
        var cacheKey = "dados2";
        var cacheDados = await _cache.GetStringAsync(cacheKey);

        if (string.IsNullOrEmpty(cacheDados)) 
        {
            cacheDados = "Dados importantes sem valor";
            var cacheOptions = new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) };
            
            await _cache.SetStringAsync(cacheKey, cacheDados, cacheOptions);
        }

        return Ok(cacheDados);
    }
}
