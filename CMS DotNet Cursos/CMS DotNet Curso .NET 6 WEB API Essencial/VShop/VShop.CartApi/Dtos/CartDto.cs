namespace VShop.CartApi.Dtos;

public class CartDto
{
    public CartHeaderDto CartHeader { get; set; } = new CartHeaderDto();
    public IEnumerable<CartItemDto> CartItems { get; set; } = Enumerable.Empty<CartItemDto>();
}
