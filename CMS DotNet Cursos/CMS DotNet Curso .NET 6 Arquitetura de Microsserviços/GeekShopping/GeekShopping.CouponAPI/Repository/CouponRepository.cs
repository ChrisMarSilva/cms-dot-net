using AutoMapper;
using GeekShopping.CouponAPI.Data.ValueObjects;
using GeekShopping.CouponAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CouponAPI.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly ILogger<CouponRepository> _logger;
        private readonly MySQLContext _context;
        private IMapper _mapper;

        public CouponRepository(
            ILogger<CouponRepository> logger,
            MySQLContext context, 
            IMapper mapper
            )
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _logger.LogInformation("CouponAPI.CouponRepository");
        }

        public async Task<CouponVO> GetCouponByCouponCode(string couponCode)
        {
            _logger.LogInformation("CouponAPI.CouponRepository.GetCouponByCouponCode()");

            var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.CouponCode == couponCode);
            
            var resultado = _mapper.Map<CouponVO>(coupon);
            return resultado;
        }
    }
}
