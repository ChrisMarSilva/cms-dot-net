using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VShop.ProductApi.Dtos;
using VShop.ProductApi.Roles;
using VShop.ProductApi.Services.Interfaces;

namespace VShop.ProductApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> Get()
    {
        var produtosDto = await _productService.GetProductsAsync();

        if (produtosDto == null)
            return NotFound("Products not found");

        return Ok(produtosDto);
    }

    [HttpGet("{id}", Name = "GetProduct")]
    public async Task<ActionResult<ProductDto>> Get(int id)
    {
        var produtoDto = await _productService.GetProductByIdAsync(id: id);

        if (produtoDto == null)
            return NotFound("Product not found");

        return Ok(produtoDto);
    }

    [HttpPost]
    [Authorize(Roles = Role.Admin)]
    public async Task<ActionResult> Post([FromBody] ProductDto produtoDto)
    {
        if (produtoDto == null)
            return BadRequest("Data Invalid");

        await _productService.AddProductAsync(productDto: produtoDto);

        return new CreatedAtRouteResult("GetProduct", new { id = produtoDto.Id }, produtoDto);
    }

    [HttpPut]
    [Authorize(Roles = Role.Admin)]
    public async Task<ActionResult<ProductDto>> Put([FromBody] ProductDto produtoDto)
    {
        if (produtoDto == null)
            return BadRequest("Data invalid");

        await _productService.UpdateProductAsync(productDto: produtoDto);

        return Ok(produtoDto);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = Role.Admin)]
    public async Task<ActionResult<ProductDto>> Delete(int id)
    {
        var produtoDto = await _productService.GetProductByIdAsync(id: id);

        if (produtoDto == null)
            return NotFound("Product not found");

        await _productService.RemoveProductAsync(id: id);

        return Ok(produtoDto);
    }
}
