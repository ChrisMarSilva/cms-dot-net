using AutoMapper;
using GeekShopping.ProductAPI.Controllers;
using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Model;
using GeekShopping.ProductAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ILogger<ProductController> _logger;
        private readonly MySQLContext _context;
        private IMapper _mapper;

        public ProductRepository(ILogger<ProductController> logger, MySQLContext context, IMapper mapper)
        {
            _logger = logger;
            this._context = context;
            this._mapper = mapper;
            _logger.LogInformation("ProductAPI.ProductRepository");
        }

        public async Task<IEnumerable<ProductVO>> FindAll()
        {
            _logger.LogInformation("ProductAPI.ProductRepository.FindAll()");
            var products = await _context.Products.ToListAsync();
            var resultado = _mapper.Map<List<ProductVO>>(products);
            return resultado;
        }

        public async Task<ProductVO> FindById(long id)
        {
            _logger.LogInformation("ProductAPI.ProductRepository.FindById()");
            var product = await _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync() ?? new Product();
            var resultado = _mapper.Map<ProductVO>(product);
            return resultado;
        }

        public async Task<ProductVO> Create(ProductVO vo)
        {
            _logger.LogInformation("ProductAPI.ProductRepository.Create()");
            var product = _mapper.Map<Product>(vo);
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            var resultado = _mapper.Map<ProductVO>(product);
            return resultado;
        }

        public async Task<ProductVO> Update(ProductVO vo)
        {
            _logger.LogInformation("ProductAPI.ProductRepository.Update()");
            var product = _mapper.Map<Product>(vo);
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            var resultado = _mapper.Map<ProductVO>(product);
            return resultado;
        }

        public async Task<bool> Delete(long id)
        {
            _logger.LogInformation("ProductAPI.ProductRepository.Delete()");
            try
            {
                var product = await _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync() ?? new Product();
                if (product.Id <= 0) 
                    return false;
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
