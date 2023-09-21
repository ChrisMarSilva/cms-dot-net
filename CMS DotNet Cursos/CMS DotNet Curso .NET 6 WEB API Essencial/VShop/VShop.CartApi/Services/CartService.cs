using AutoMapper;
using VShop.CartApi.Dtos;
using VShop.CartApi.Models;
using VShop.CartApi.Repositories.Interfaces;
using VShop.CartApi.Services.Interfaces;

namespace VShop.CartApi.Services;

public class CartService : ICartService
{
    private ICartRepository _cartRepository;
    private readonly IMapper _mapper;

    public CartService(ICartRepository cartRepository, IMapper mapper)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
    }

    public Task<IEnumerable<CartDto>?> GetCartsAsync()
    {
        throw new NotImplementedException();
        //var categoriesModel = await _categoryRepository.GetAllAsync();

        //var categories = _mapper.Map<IEnumerable<CategoryDto>>(categoriesModel);
        //return categories;
    }

    public Task<CartDto?> GetCartByIdAsync(int id)
    {
        throw new NotImplementedException();
        //var categoryModel = await _categoryRepository.GetByIdAsync(id: id);

        //var category = _mapper.Map<CategoryDto>(categoryModel);
        //return category;
    }

    public Task AddCartAsync(CartDto cartDto)
    {
        throw new NotImplementedException();
        //var categoryModel = _mapper.Map<CategoryModel>(categoryDto);

        //await _categoryRepository.CreateAsync(category: categoryModel);
        //categoryDto.CategoryId = categoryModel.CategoryId;
    }

    public Task UpdateCartAsync(CartDto cartDto)
    {
        throw new NotImplementedException();
        //var categoryModel = _mapper.Map<CategoryModel>(categoryDto);

        //await _categoryRepository.UpdateAsync(category: categoryModel);
    }

    public Task RemoveCartAsync(int id)
    {
        throw new NotImplementedException();
        //var categoryModel = _categoryRepository.GetByIdAsync(id: id).Result;

        //if (categoryModel is not null)
        //    await _categoryRepository.DeleteAsync(id: categoryModel.CategoryId);
    }
}
