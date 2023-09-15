using AutoMapper;
using VShop.ProductApi.Dtos;
using VShop.ProductApi.Models;
using VShop.ProductApi.Repositories.Interfaces;
using VShop.ProductApi.Services.Interfaces;

namespace VShop.ProductApi.Services;

public class ProductService : IProductService
{
    private readonly IMapper _mapper;
    private IProductRepository _productRepository;

    public ProductService(IMapper mapper, IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductDto>> GetProductsAsync()
    {
        var productsModel = await _productRepository.GetAllAsync();

        var products = _mapper.Map<IEnumerable<ProductDto>>(productsModel);
        return products;
    }

    public async Task<ProductDto> GetProductByIdAsync(int id)
    {
        var productModel = await _productRepository.GetByIdAsync(id: id);

        var product = _mapper.Map<ProductDto>(productModel);
        return product;
    }
    public async Task AddProductAsync(ProductDto productDto)
    {
        var productModel = _mapper.Map<ProductModel>(productDto);

        await _productRepository.CreateAsync(product: productModel);
        productDto.Id = productModel.Id;
    }

    public async Task UpdateProductAsync(ProductDto productDto)
    {
        var productModel = _mapper.Map<ProductModel>(productDto);

        await _productRepository.UpdateAsync(product: productModel);
    }

    public async Task RemoveProductAsync(int id)
    {
        var productModel = await _productRepository.GetByIdAsync(id: id);

        if (productModel is not null)
            await _productRepository.DeleteAsync(id: productModel.Id);
    }
}
