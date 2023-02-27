using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Repository;
using GeekShopping.ProductAPI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.ProductAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private IProductRepository _repository;

        public ProductController(
            ILogger<ProductController> logger, 
            IProductRepository repository
            )
        {
            _logger = logger;
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger.LogInformation("ProductAPI.ProductController");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductVO>>> FindAll()
        {
            _logger.LogInformation("ProductAPI.ProductController.FindAll()");
            var products = await _repository.FindAll();
            return Ok(products);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ProductVO>> FindById(long id)
        {
            _logger.LogInformation("ProductAPI.ProductController.FindById()");
            var product = await _repository.FindById(id);
            if (product.Id <= 0)
                return NotFound();
            return Ok(product);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ProductVO>> Create([FromBody] ProductVO vo)
        {
            _logger.LogInformation("ProductAPI.ProductController.Create()");
            if (vo == null)
                return BadRequest();
            var product = await _repository.Create(vo);
            return Ok(product);
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<ProductVO>> Update([FromBody] ProductVO vo)
        {
            _logger.LogInformation("ProductAPI.ProductController.Update()");
            if (vo == null)
                return BadRequest();
            var product = await _repository.Update(vo);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<ProductVO>> Delete(long id)
        {
            _logger.LogInformation("ProductAPI.ProductController.Delete()");
            var status = await _repository.Delete(id);
            if (!status) 
                return BadRequest();
            return Ok(status);
        }

    }
}
