using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;
// using StackExchange.Redis;
// using System.Net;
// using Microsoft.Extensions.Caching.Memory;

namespace WebApplication1.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProcController : ControllerBase
    {
        private readonly ILogger<ProcController> _logger;
        private readonly ICacheService _cacheService;
        //private readonly IConnectionMultiplexer _connectionMultiplexer;
        // private readonly IMemoryCache _memoryCache;
        // private static MemoryCacheEntryOptions _cacheEntryOptions;

        public ProcController(
            ILogger<ProcController> logger,
            ICacheService cacheService
            /* IConnectionMultiplexer connectionMultiplexer */
            /* IMemoryCache memoryCache*/
            )
        {
            _logger = logger;
            _cacheService = cacheService;
            // _connectionMultiplexer = connectionMultiplexer;
            // _memoryCache = memoryCache;
            // _cacheEntryOptions = GetCacheEntryOptions();
        }

        [HttpPost("")]
        public async Task<IActionResult> SetAsync(string key, string value)
        {
            try
            {
                _logger.LogInformation($"CALL: SetAsync - key={key} - value={value}");

                // _memoryCache.Set(key, value, MemoryCacheEntryOptions(){ AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(40), SlidingExpiration = TimeSpan.FromSeconds(10)});
                //_memoryCache.Set(key, value, _cacheEntryOptions);

                // var db = _connectionMultiplexer.GetDatabase();
                // await db.StringSetAsync(key, value);
                // await db.StringSetAsync(key, value, flags: CommandFlags.FireAndForget);

                await _cacheService.SetCacheValueAsync(key, value);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"FAILED: SetAsync - key={key} - value={value} - erro={ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                //return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error. Something went Wrong!");
            }
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAsync(string key)
        {
            try
            {
                _logger.LogInformation($"CALL: GetAsync - key={key}");

                // if (!_memoryCache.TryGetValue(key, out string value))
                //     return NotFound();
                // var db = _connectionMultiplexer.GetDatabase();
                //string value = await db.StringGetAsync(key);

                string value = await _cacheService.GetCacheValueAsync(key);

                _logger.LogInformation($"CALL: GetAsync - key={key} - value={value}");

                // return string.IsNullOrEmpty(value) ? (IActionResult) NotFound() : Ok(value);

                if (string.IsNullOrEmpty(value))
                {
                    // value = "123"; // valor default // ou pegar do Banco de Dados
                    // await db.StringSetAsync(key, value);
                    return NotFound();
                }

                return Ok(value);
            }
            catch (Exception ex)
            {
                _logger.LogError($"FAILED: GetAsync - key={key} - erro={ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                //return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error. Something went Wrong!");
            }
        }

        // private static MemoryCacheEntryOptions GetCacheEntryOptions()
        // {
        //     return new MemoryCacheEntryOptions()
        //         .SetAbsoluteExpiration(TimeSpan.FromSeconds(100)) // 100 // 40
        //         .SetSlidingExpiration(TimeSpan.FromSeconds(100)); // 100 // 10
        // }

    }
}


//public interface ICacheAccessor
//{
//    Task SetAsync(string value, string key);
//    Task<string> GetAsync(string key);
//}
