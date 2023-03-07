using PokeAcademy.API.Models;
using Refit;

namespace PokeAcademy.API.Services.Interfaces
{
    public interface IPokeService
    {
        [Get("/pokemon")]
        Task<NamedAPIResourceList> GetAllAsync(int limit);

        [Get("/pokemon/{id}")]
        Task<PokemonData> GetByIdAsync(int id);
    }
}
