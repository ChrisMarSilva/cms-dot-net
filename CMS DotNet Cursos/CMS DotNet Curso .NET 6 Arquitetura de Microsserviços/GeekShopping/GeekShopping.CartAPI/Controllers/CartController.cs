using GeekShopping.CartAPI.Data.ValueObjects;
using GeekShopping.CartAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CartAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ILogger<CartController> _logger;
        private ICartRepository _repository;

        public CartController(
            ILogger<CartController> logger,
            ICartRepository repository
            )
        {
            _logger = logger;
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger.LogInformation("CartAPI.CartController");
        }

        [HttpGet("find-cart/{id}")]
        public async Task<ActionResult<CartVO>> FindById(string userId)
        {
            _logger.LogInformation("CartAPI.CartController.FindAll()");
            var cart = await _repository.FindCartByUserId(userId);
            if (cart == null) 
                return NotFound();
            return Ok(cart);
        }

        [HttpPost("add-cart/{id}")]
        public async Task<ActionResult<CartVO>> AddCart(CartVO vo)
        {
            _logger.LogInformation("CartAPI.CartController.AddCart()");
            var cart = await _repository.SaveOrUpdateCart(vo);
            if (cart == null) 
                return NotFound();
            return Ok(cart);
        }

        [HttpPut("update-cart/{id}")]
        public async Task<ActionResult<CartVO>> UpdateCart(CartVO vo)
        {
            _logger.LogInformation("CartAPI.CartController.UpdateCart()");
            var cart = await _repository.SaveOrUpdateCart(vo);
            if (cart == null) 
                return NotFound();
            return Ok(cart);
        }

        [HttpDelete("remove-cart/{id}")]
        public async Task<ActionResult<CartVO>> RemoveCart(int id)
        {
            _logger.LogInformation("CartAPI.CartController.RemoveCart()");
            var status = await _repository.RemoveFromCart(id);
            if (!status) 
                return BadRequest();
            return Ok(status);
        }
    }
}
