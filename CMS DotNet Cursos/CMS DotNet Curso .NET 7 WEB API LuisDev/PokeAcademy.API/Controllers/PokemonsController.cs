using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using PokeAcademy.API.Models;
using PokeAcademy.API.Services.Interfaces;
using PokeAcademy.API.Utils;
using System.Net.Http.Headers;

namespace PokeAcademy.API.Controllers
{
    [Route("api/pokemon")] // [Route("api/[controller]")]
    [ApiController]
    public class PokemonsController : ControllerBase
    {

        private readonly ILogger<PokemonsController> _logger;
        private readonly IPokeService _pokeService;
        private readonly HttpClient _client; // _httpClient 

        public PokemonsController(
            ILogger<PokemonsController> logger,
            HttpClient client,
            IPokeService pokeService
            )
        {
            _logger = logger;
            _client = client;// ?? throw new ArgumentNullException(nameof(client));
            //_client = clientFactory.createClient("someClient");
            //_client.DefaultRequestHeaders.Accept.Clear();
            //_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _pokeService = pokeService;
        }

        //[ActionName("GetAllRefit")]
        [Route("GetAllRefit")]
        [HttpGet]
        public async Task<IActionResult> GetAllRefit(int limit)
        {
            var result = await _pokeService.GetAllAsync(limit);

            var viewModelList = result.MapToViewModel();

            return Ok(viewModelList);
        }

        //[ActionName("GetAllHttpClient")]
        [Route("GetAllHttpClient")]
        [HttpGet]
        public async Task<IActionResult> GetAllHttpClient(int limit)
        {
            var response = await _client.GetAsync(""); // "https://pokeapi.co/api/v2"

            var result = await response.ReadContentAs<NamedAPIResourceList>();

            var viewModelList = result.MapToViewModel();

            return Ok(viewModelList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _pokeService.GetByIdAsync(id);
            return Ok(result);
        }

    }
}
