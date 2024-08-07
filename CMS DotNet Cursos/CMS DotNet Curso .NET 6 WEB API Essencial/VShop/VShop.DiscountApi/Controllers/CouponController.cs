﻿using Microsoft.AspNetCore.Mvc;
using VShop.DiscountApi.Dtos;
using VShop.DiscountApi.Repositories.Interfaces;

namespace VShop.DiscountApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CouponController : ControllerBase
{
    private ICouponRepository _repository;

    public CouponController(ICouponRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("{couponCode}")]
    public async Task<ActionResult<CouponDto>> GetDiscountCouponByCode(string couponCode)
    {
        var coupon = await _repository
            .GetCouponByCode(couponCode);

        if (coupon is null)
            return NotFound($"Coupon Code: {couponCode} not found");

        return Ok(coupon);
    }
}
