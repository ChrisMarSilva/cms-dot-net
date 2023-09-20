using AutoMapper;
using VShop.CartApi.Models;

namespace VShop.CartApi.Dtos.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CartDto, Cart>().ReverseMap();
        CreateMap<CartHeaderDto, CartHeader>().ReverseMap();
        CreateMap<CartItemDto, CartItem>().ReverseMap();
        CreateMap<ProductDto, Product>().ReverseMap();
    }
}
