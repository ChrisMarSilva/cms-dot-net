using VShop.Web.Models;

namespace VShop.Web.Services.Contracts;

public interface ICouponService
{
    Task<CouponViewModel?> GetDiscountCouponAsync(string couponCode, string token);
}

