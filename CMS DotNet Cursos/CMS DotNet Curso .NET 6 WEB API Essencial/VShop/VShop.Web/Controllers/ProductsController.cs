using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VShop.Web.Models;
using VShop.Web.Roles;
using VShop.Web.Services.Contracts;

namespace VShop.Web.Controllers;

[Authorize(Roles = Role.Admin)]
public class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public ProductsController(IProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }
    private async Task<string> GetAccessTokenAsync()
    {
        var token = await HttpContext.GetTokenAsync(tokenName: "access_token");

        return token ?? "";
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductViewModel>>> Index()
    {
        var token = await GetAccessTokenAsync();
        var result = await _productService.GetAllProductsAsync(token: token);

        if (result is null)
            return View("Error");

        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> CreateProduct()
    {
        var token = await GetAccessTokenAsync();
        var items = await _categoryService.GetAllCategoriesAsync(token: token);

        ViewBag.CategoryId = new SelectList(items: items, dataValueField: "CategoryId", dataTextField: "Name");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(ProductViewModel productVM)
    {
        var token = await GetAccessTokenAsync();

        if (ModelState.IsValid)
        {
            var result = await _productService.CreateProductAsync(productVM: productVM, token: token);

            if (result != null)
                return RedirectToAction(actionName: nameof(Index));
        }
        else
        {
            var items = await _categoryService.GetAllCategoriesAsync(token: token);
            ViewBag.CategoryId = new SelectList(items: items, dataValueField: "CategoryId", dataTextField: "Name");
        }

        return View(productVM);
    }

    [HttpGet]
    public async Task<IActionResult> UpdateProduct(int id)
    {
        var token = await GetAccessTokenAsync();
        var items = await _categoryService.GetAllCategoriesAsync(token: token);

        ViewBag.CategoryId = new SelectList(items: items, dataValueField: "CategoryId", dataTextField: "Name");

        var result = await _productService.FindProductByIdAsync(id: id, token: token);

        if (result is null)
            return View("Error");

        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateProduct(ProductViewModel productVM)
    {
        if (ModelState.IsValid)
        {
            var token = await GetAccessTokenAsync();
            var result = await _productService.UpdateProductAsync(productVM: productVM, token: token);

            if (result is not null)
                return RedirectToAction(actionName: nameof(Index));
        }

        return View(productVM);
    }

    [HttpGet]
    public async Task<ActionResult<ProductViewModel>> DeleteProduct(int id)
    {
        var token = await GetAccessTokenAsync();
        var result = await _productService.FindProductByIdAsync(id: id, token: token);

        if (result is null)
            return View("Error");

        return View(result);
    }

    [HttpPost(), ActionName("DeleteProduct")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var token = await GetAccessTokenAsync();
        var result = await _productService.DeleteProductByIdAsync(id: id, token: token);

        if (!result)
            return View("Error");

        return RedirectToAction(actionName: "Index");
    }
}
