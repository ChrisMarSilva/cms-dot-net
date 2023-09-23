using Microsoft.AspNetCore.Mvc;
using VShop.CartApi.Dtos;
using VShop.CartApi.Repositories.Interfaces;
using VShop.CartApi.Services.Interfaces;

namespace VShop.CartApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartService _service;
    private readonly ICartRepository _repository;

    public CartController(ICartService service, ICartRepository repository)
    {
        _service = service;
        _repository = repository;
    }

    [HttpGet("getcart/{userid}")]
    public async Task<ActionResult<CartDto>> GetByUserId(string userid)
    {
        var cartDto = await _repository
            .GetCartByUserIdAsync(userId: userid);

        if (cartDto is null)
            return NotFound();

        return Ok(cartDto);
    }

    [HttpPost("addcart")]
    public async Task<ActionResult<CartDto>> AddCart(CartDto cartDto)
    {
        var cart = await _repository
            .UpdateCartAsync(cart: cartDto);

        if (cart is null)
            return NotFound();

        return Ok(cart);
    }

    [HttpPut("updatecart")]
    public async Task<ActionResult<CartDto>> UpdateCart(CartDto cartDto)
    {
        var cart = await _repository
            .UpdateCartAsync(cart: cartDto);

        if (cart == null)
            return NotFound();

        return Ok(cart);
    }

    [HttpDelete("deletecart/{id}")]
    public async Task<ActionResult<bool>> DeleteCart(int id)
    {
        var status = await _repository
            .DeleteItemCartAsync(cartItemId: id);

        if (!status)
            return BadRequest();

        return Ok(status);
    }

    [HttpPost("applycoupon")]
    public async Task<ActionResult<CartDto>> ApplyCoupon(CartDto cartDto)
    {
        var result = await _repository.ApplyCouponAsync(
            userId: cartDto.CartHeader.UserId,
            couponCode: cartDto.CartHeader.CouponCode);

        if (!result)
            return NotFound($"CartHeader not found for userId = {cartDto.CartHeader.UserId}");

        return Ok(result);
    }

    [HttpDelete("deletecoupon/{userId}")]
    public async Task<ActionResult<CartDto>> DeleteCoupon(string userId)
    {
        var result = await _repository
            .DeleteCouponAsync(userId: userId);

        if (!result)
            return NotFound($"Discount Coupon not found for userId = {userId}");

        return Ok(result);
    }

    [HttpPost("checkout")]
    public async Task<ActionResult<CheckoutHeaderDto>> Checkout(CheckoutHeaderDto checkoutDto)
    {
        var cart = await _repository.
            GetCartByUserIdAsync(userId: checkoutDto.UserId);

        if (cart is null)
            return NotFound($"Cart Not found for {checkoutDto.UserId}");

        checkoutDto.CartItems = cart.CartItems;
        checkoutDto.DateTime = DateTime.Now;

        return Ok(checkoutDto);
    }
}
