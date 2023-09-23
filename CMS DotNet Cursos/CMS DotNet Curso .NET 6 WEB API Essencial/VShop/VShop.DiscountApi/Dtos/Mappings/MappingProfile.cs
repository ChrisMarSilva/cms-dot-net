using AutoMapper;
using VShop.DiscountApi.Models;

namespace VShop.DiscountApi.Dtos.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CouponDto, Coupon>().ReverseMap();
    }
}