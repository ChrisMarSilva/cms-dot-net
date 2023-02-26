using GeekShopping.CartAPI.Data.ValueObjects;
using GeekShopping.CartAPI.Messages;
using GeekShopping.CartAPI.Repository;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<ActionResult<CartVO>> FindById(string id)
        {
            _logger.LogInformation("CartAPI.CartController.FindAll()");

            var cart = await _repository.FindCartByUserId(id);

            if (cart == null) 
                return NotFound();

            return Ok(cart);
        }

        [HttpPost("add-cart")]
        [Authorize]
        public async Task<ActionResult<CartVO>> AddCart(CartVO vo)
        {
            _logger.LogInformation("CartAPI.CartController.AddCart()");

            var cart = await _repository.SaveOrUpdateCart(vo);

            if (cart == null) 
                return NotFound();

            return Ok(cart);
        }

        [HttpPut("update-cart")]
        [Authorize]
        public async Task<ActionResult<CartVO>> UpdateCart(CartVO vo)
        {
            _logger.LogInformation("CartAPI.CartController.UpdateCart()");

            var cart = await _repository.SaveOrUpdateCart(vo);

            if (cart == null) 
                return NotFound();

            return Ok(cart);
        }

        [HttpDelete("remove-cart/{id}")]
        [Authorize]
        public async Task<ActionResult<CartVO>> RemoveCart(int id)
        {
            _logger.LogInformation("CartAPI.CartController.RemoveCart()");

            var status = await _repository.RemoveFromCart(id);

            if (!status) 
                return BadRequest();

            return Ok(status);
        }


        [HttpPost("apply-coupon")]
        public async Task<ActionResult<CartVO>> ApplyCoupon(CartVO vo)
        {
            _logger.LogInformation("CartAPI.CartController.ApplyCoupon()");

            var status = await _repository.ApplyCoupon(vo.CartHeader.UserId, vo.CartHeader.CouponCode);

            if (!status) 
                return NotFound();

            return Ok(status);
        }

        [HttpDelete("remove-coupon/{userId}")]
        public async Task<ActionResult<CartVO>> ApplyCoupon(string userId)
        {
            _logger.LogInformation("CartAPI.CartController.ApplyCoupon()");

            var status = await _repository.RemoveCoupon(userId);

            if (!status) 
                return NotFound();

            return Ok(status);
        }

        [HttpPost("checkout")]
        public async Task<ActionResult<CheckoutHeaderVO>> Checkout(CheckoutHeaderVO vo)
        {
            _logger.LogInformation("CartAPI.CartController.Checkout()");

            if (vo?.UserId == null)
                return NotFound();

            var cart = await _repository.FindCartByUserId(vo.UserId);

            if (cart == null) 
                return NotFound();

            vo.CartDetails = cart.CartDetails;
            vo.DateTime = DateTime.Now;

            //TASK RabbitMQ logic comes here!!!

            return Ok(vo);
        }
    }
}
