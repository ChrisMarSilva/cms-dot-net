using VShop.CartApi.Dtos;

namespace VShop.CartApi.Services.Interfaces;

public interface ICartService
{
    Task<IEnumerable<CartDto>?> GetCartsAsync();
    Task<CartDto?> GetCartByIdAsync(int id);
    Task AddCartAsync(CartDto cartDto);
    Task UpdateCartAsync(CartDto cartDto);
    Task RemoveCartAsync(int id);
}
