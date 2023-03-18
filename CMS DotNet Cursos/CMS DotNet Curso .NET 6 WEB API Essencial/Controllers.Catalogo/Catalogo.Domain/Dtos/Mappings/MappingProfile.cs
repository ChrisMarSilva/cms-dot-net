using AutoMapper;
using Catalogo.Domain.Models;

namespace Catalogo.Domain.Dtos.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //Produto
        CreateMap<ProdutoRequestDTO, Produto>();
        CreateMap<Produto, ProdutoResponseDTO>();
        //CreateMap<Produto, ProdutoResponseDTO>().ReverseMap();

        //Categoria
        CreateMap<CategoriaRequestDTO, Categoria>();
        CreateMap<Categoria, CategoriaResponseDTO>();
        //CreateMap<Categoria, CategoriaResponseDTO>().ReverseMap();
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
