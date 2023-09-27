using VShop.DiscountApi.Dtos;

namespace VShop.DiscountApi.Repositories.Interfaces;

public interface ICouponRepository
{
    Task<CouponDto> GetCouponByCode(string couponCode);
}
