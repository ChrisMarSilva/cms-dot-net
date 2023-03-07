namespace PokeAcademy.API.Models
{
    public class PokemonListViewModel
    {
        public int Count { get; set; }
        public IEnumerable<PokemonListItemViewModel> Pokemons { get; set; }
    }
}
