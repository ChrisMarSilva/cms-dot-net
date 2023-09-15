using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VShop.ProductApi.Dtos;
using VShop.ProductApi.Roles;
using VShop.ProductApi.Services.Interfaces;

namespace VShop.ProductApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> Get()
    {
        var categoriesDto = await _categoryService.GetCategoriesAsync();

        if (categoriesDto == null)
            return NotFound("Categories not found");

        return Ok(categoriesDto);
    }

    [HttpGet("products")]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategoriasProducts()
    {
        var categoriesDto = await _categoryService.GetCategoriesProductsAsync();

        if (categoriesDto == null)
            return NotFound("Categories not found");

        return Ok(categoriesDto);
    }

    [HttpGet("{id:int}", Name = "GetCategory")]
    public async Task<ActionResult<CategoryDto>> Get(int id)
    {
        var categoryDto = await _categoryService.GetCategoryByIdAsync(id: id);

        if (categoryDto == null)
            return NotFound("Category not found");

        return Ok(categoryDto);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CategoryDto categoryDto)
    {
        if (categoryDto == null)
            return BadRequest("Invalid Data");

        await _categoryService.AddCategoryAsync(categoryDto: categoryDto);

        return new CreatedAtRouteResult("GetCategory", new { id = categoryDto.CategoryId }, categoryDto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, [FromBody] CategoryDto categoryDto)
    {
        if (id != categoryDto.CategoryId)
            return BadRequest();

        if (categoryDto == null)
            return BadRequest();

        await _categoryService.UpdateCategoryAsync(categoryDto: categoryDto);

        return Ok(categoryDto);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = Role.Admin)]
    public async Task<ActionResult<CategoryDto>> Delete(int id)
    {
        var categoryDto = await _categoryService.GetCategoryByIdAsync(id: id);

        if (categoryDto == null)
            return NotFound("Category not found");

        await _categoryService.RemoveCategoryAsync(id: id);

        return Ok(categoryDto);
    }
}
