﻿using GeekShopping.CartAPI.Data.ValueObjects;
using GeekShopping.CartAPI.Messages;
using GeekShopping.CartAPI.RabbitMQSender;
using GeekShopping.CartAPI.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CartAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ILogger<CartController> _logger;
        private ICartRepository _cartRepository;
        private ICouponRepository _couponRepository;
        private IRabbitMQMessageSender _rabbitMQMessageSender;

        public CartController(
            ILogger<CartController> logger,
            ICartRepository cartRepository,
            ICouponRepository couponRepository,
            IRabbitMQMessageSender rabbitMQMessageSender
            )
        {
            _logger = logger;
            _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
            _couponRepository = couponRepository ?? throw new ArgumentNullException(nameof(couponRepository));
            _rabbitMQMessageSender = rabbitMQMessageSender ?? throw new ArgumentNullException(nameof(rabbitMQMessageSender));
            _logger.LogInformation("CartAPI.CartController");
        }

        [HttpGet("find-cart/{id}")]
        [Authorize]
        public async Task<ActionResult<CartVO>> FindById(string id)
        {
            _logger.LogInformation("CartAPI.CartController.FindAll()");

            var cart = await _cartRepository.FindCartByUserId(id);

            if (cart == null) 
                return NotFound();

            return Ok(cart);
        }

        [HttpPost("add-cart")]
        [Authorize]
        public async Task<ActionResult<CartVO>> AddCart(CartVO vo)
        {
            _logger.LogInformation("CartAPI.CartController.AddCart()");

            var cart = await _cartRepository.SaveOrUpdateCart(vo);

            if (cart == null) 
                return NotFound();

            return Ok(cart);
        }

        [HttpPut("update-cart")]
        [Authorize]
        public async Task<ActionResult<CartVO>> UpdateCart(CartVO vo)
        {
            _logger.LogInformation("CartAPI.CartController.UpdateCart()");

            var cart = await _cartRepository.SaveOrUpdateCart(vo);

            if (cart == null) 
                return NotFound();

            return Ok(cart);
        }

        [HttpDelete("remove-cart/{id}")]
        [Authorize]
        public async Task<ActionResult<CartVO>> RemoveCart(int id)
        {
            _logger.LogInformation("CartAPI.CartController.RemoveCart()");

            var status = await _cartRepository.RemoveFromCart(id);

            if (!status) 
                return BadRequest();

            return Ok(status);
        }


        [HttpPost("apply-coupon")]
        public async Task<ActionResult<CartVO>> ApplyCoupon(CartVO vo)
        {
            _logger.LogInformation("CartAPI.CartController.ApplyCoupon()");

            var status = await _cartRepository.ApplyCoupon(vo.CartHeader.UserId, vo.CartHeader.CouponCode);

            if (!status) 
                return NotFound();

            return Ok(status);
        }

        [HttpDelete("remove-coupon/{userId}")]
        public async Task<ActionResult<CartVO>> ApplyCoupon(string userId)
        {
            _logger.LogInformation("CartAPI.CartController.ApplyCoupon()");

            var status = await _cartRepository.RemoveCoupon(userId);

            if (!status) 
                return NotFound();

            return Ok(status);
        }

        [HttpPost("checkout")]
        public async Task<ActionResult<CheckoutHeaderVO>> Checkout(CheckoutHeaderVO vo)
        {
            _logger.LogInformation("CartAPI.CartController.Checkout()");

            if (vo?.UserId == null)
                return BadRequest();

            var cart = await _cartRepository.FindCartByUserId(vo.UserId);

            if (cart == null) 
                return NotFound();

            if (!string.IsNullOrEmpty(vo.CouponCode))
            {
                // string token = Request.Headers["Authorization"]; // erro
                var token = await HttpContext.GetTokenAsync("access_token");
                
                CouponVO coupon = await _couponRepository.GetCoupon(vo.CouponCode, token);
                
                if (vo.DiscountAmount != coupon.DiscountAmount)
                    return StatusCode(412); // 412 Precondition Failed
            }

            vo.CartDetails = cart.CartDetails;
            vo.DateTime = DateTime.Now;

            // RabbitMQ logic comes here!!!
            _rabbitMQMessageSender.SendMessage(vo, "checkoutqueue");

            await _cartRepository.ClearCart(vo.UserId);

            return Ok(vo);
        }
    }
}
