using AutoMapper;
using VShop.ProductApi.Models;

namespace VShop.ProductApi.Dtos.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CategoryModel, CategoryDto>().ReverseMap();

        CreateMap<ProductDto, ProductModel>();
        CreateMap<ProductModel, ProductDto>()
            .ForMember(x => x.CategoryName, opt => opt.MapFrom(src => src!.Category!.Name));
    }
}
