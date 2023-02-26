using GeekShopping.CouponAPI.Data.ValueObjects;
using GeekShopping.CouponAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CouponAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ILogger<CouponController> _logger;
        private ICouponRepository _repository;

        public CouponController(
            ILogger<CouponController> logger,
            ICouponRepository repository
            )
        {
            _logger = logger;
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger.LogInformation("CouponAPI.CouponController");
        }

        [HttpGet("{couponCode}")]
        [Authorize]
        public async Task<ActionResult<CouponVO>> GetCouponByCouponCode(string couponCode)
        {
            _logger.LogInformation("CouponAPI.CouponController.GetCouponByCouponCode()");
           
            var coupon = await _repository.GetCouponByCouponCode(couponCode);
            
            if (coupon == null) 
                return NotFound();

            return Ok(coupon);
        }
    }
}
