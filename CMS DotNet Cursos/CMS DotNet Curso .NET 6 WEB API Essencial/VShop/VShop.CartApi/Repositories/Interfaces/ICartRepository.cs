using VShop.CartApi.Dtos;

namespace VShop.CartApi.Repositories.Interfaces;

public interface ICartRepository
{
    Task<CartDto> GetCartByUserIdAsync(string userId);
    Task<CartDto> UpdateCartAsync(CartDto cart);
    Task<bool> CleanCartAsync(string userId);
    Task<bool> DeleteItemCartAsync(int cartItemId);
    Task<bool> ApplyCouponAsync(string userId, string couponCode);
    Task<bool> DeleteCouponAsync(string userId);
}

