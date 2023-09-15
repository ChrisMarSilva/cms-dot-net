using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VShop.Web.Models;
using VShop.Web.Services.Contracts;

namespace VShop.Web.Controllers;

public class CartController : Controller
{
    private readonly ICartService _cartService;
    private readonly ICouponService _couponService;

    public CartController(ICartService cartService, ICouponService couponService)
    {
        _cartService = cartService;
        _couponService = couponService;
    }

    [HttpGet]
    public async Task<IActionResult> Checkout()
    {
        var cartVM = await GetCartByUser();

        return View(cartVM);
    }

    [HttpPost]
    public async Task<IActionResult> Checkout(CartViewModel cartVM)
    {
        if (ModelState.IsValid)
        {
            var token = await GetAccessToken();
            var result = await _cartService.CheckoutAsync(cartHeader: cartVM.CartHeader, token: token);

            if (result is not null)
                return RedirectToAction(actionName: nameof(CheckoutCompleted));
        }

        return View(cartVM);
    }

    [HttpGet]
    public IActionResult CheckoutCompleted()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ApplyCoupon(CartViewModel cartVM)
    {
        if (ModelState.IsValid)
        {
            var token = await GetAccessToken();
            var result = await _cartService.ApplyCouponAsync(cartVM: cartVM, token: token);

            if (result)
                return RedirectToAction(actionName: nameof(Index));
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> DeleteCoupon()
    {
        var token = await GetAccessToken();
        var userId = GetUserId();

        var result = await _cartService.RemoveCouponAsync(userId: userId, token: token);

        if (result)
            return RedirectToAction(actionName: nameof(Index));

        return View();
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
        var cartVM = await GetCartByUser();

        if (cartVM is null)
        {
            ModelState.AddModelError("CartNotFound", "Does not exist a cart yet...Come on Shopping...");
            return View("/Views/Cart/CartNotFound.cshtml");
        }

        return View(cartVM);
    }

    private async Task<CartViewModel?> GetCartByUser()
    {
        var token = await GetAccessToken();
        var userId = GetUserId();

        var cart = await _cartService.GetCartByUserIdAsync(userId: userId, token: token);

        if (cart?.CartHeader is not null)
        {
            if (!string.IsNullOrEmpty(cart.CartHeader.CouponCode))
            {
                var coupon = await _couponService.GetDiscountCouponAsync(couponCode: cart.CartHeader.CouponCode, token: token);

                if (coupon?.CouponCode is not null)
                    cart.CartHeader.Discount = coupon.Discount;
            }

            if (cart?.CartItems is not null)
            {
                foreach (var item in cart.CartItems)
                {
                    if (item?.Product is not null)
                        cart.CartHeader.TotalAmount += item.Product.Price * item.Quantity;
                }

                cart.CartHeader.TotalAmount = cart.CartHeader.TotalAmount - (cart.CartHeader.TotalAmount * cart.CartHeader.Discount) / 100;
            }
        }

        return cart;
    }

    public async Task<IActionResult> RemoveItem(int id)
    {
        var token = await GetAccessToken();

        var result = await _cartService.RemoveItemFromCartAsync(cartId: id, token: token);

        if (result)
            return RedirectToAction(actionName: nameof(Index));

        return View(id);
    }

    private async Task<string> GetAccessToken()
    {
        var token = await HttpContext.GetTokenAsync(tokenName: "access_token");

        return token ?? "";
    }

    private string GetUserId()
    {
        return User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value ?? "";
    }
}