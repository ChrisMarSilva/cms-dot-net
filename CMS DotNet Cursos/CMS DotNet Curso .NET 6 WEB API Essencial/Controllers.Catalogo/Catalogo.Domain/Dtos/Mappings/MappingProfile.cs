using AutoMapper;
using Catalogo.Domain.Models;

namespace Catalogo.Domain.Dtos.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Produto, ProdutoDTO>().ReverseMap();
        CreateMap<Categoria, CategoriaDTO>().ReverseMap();
    }
}

//public class MappingProfile
//{
//    public static MapperConfiguration RegisterMaps()
//    {
//        var mappingConfig = new MapperConfiguration(config => {
//            CreateMap<Produto, ProdutoDTO>().ReverseMap();
//            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
//        });
//        return mappingConfig;
//    }
//}
