using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VShop.Web.Models;
using VShop.Web.Services.Contracts;

namespace VShop.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductService _productService;
    private readonly ICartService _cartService;

    public HomeController(
        ILogger<HomeController> logger,
        IProductService productService,
        ICartService cartService)
    {
        _logger = logger;
        _productService = productService;
        _cartService = cartService;
    }

    private async Task<string> GetAccessTokenAsync()
    {
        var token = await HttpContext.GetTokenAsync(tokenName: "access_token");

        return token ?? "";
    }

    private string GetUserId()
    {
        var userId = User
            .Claims
            .Where(u => u.Type == "sub")?
            .FirstOrDefault()?
            .Value;

        return userId ?? "";
    }

    public async Task<IActionResult> Index()
    {
        var token = string.Empty;

        var products = await _productService
            .GetAllProductsAsync(token: token);

        if (products is null)
            return View("Error");

        return View(products);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<ProductViewModel>> ProductDetails(int id)
    {
        var token = await GetAccessTokenAsync();

        var product = await _productService
            .FindProductByIdAsync(id: id, token: token);

        if (product is null)
            return View("Error");

        return View(product);
    }

    [Authorize]
    [HttpPost]
    [ActionName("ProductDetails")]
    public async Task<ActionResult<ProductViewModel>> ProductDetailsPost(ProductViewModel productVM)
    {
        var token = await GetAccessTokenAsync();
        var userId = GetUserId();

        CartViewModel cart = new() { CartHeader = new CartHeaderViewModel { UserId = userId } };

        var product = await _productService
            .FindProductByIdAsync(id: productVM.Id, token: token);

        CartItemViewModel cartItem = new()
        {
            Quantity = productVM.Quantity,
            ProductId = productVM.Id,
            Product = product
        };

        var cartItemsVM = new List<CartItemViewModel>();
        cartItemsVM.Add(cartItem);
        cart.CartItems = cartItemsVM;

        var result = await _cartService
            .AddItemToCartAsync(cartVM: cart, token: token);

        if (result is not null)
            return RedirectToAction(actionName: nameof(Index));

        return View(productVM);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(string message)
    {
        var result = new ErrorViewModel { ErrorMessage = message };

        return View(result);
    }

    [Authorize]
    public async Task<IActionResult> Login()
    {
        var token = await GetAccessTokenAsync();

        return RedirectToAction(actionName: nameof(Index));
    }

    public IActionResult Logout()
    {
        return SignOut("Cookies", "oidc");
    }
}