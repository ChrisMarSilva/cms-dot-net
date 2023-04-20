using AutoMapper;
using Catalogo.Application.Dtos;
using Catalogo.Domain.Models;

namespace Catalogo.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Produto
        CreateMap<ProdutoRequestDTO, Produto>();
        CreateMap<Produto, ProdutoResponseDTO>();
        // CreateMap<Produto, ProdutoResponseDTO>().ReverseMap();

        // Categoria
        CreateMap<CategoriaRequestDTO, Categoria>();
        CreateMap<Categoria, CategoriaResponseDTO>();
        // CreateMap<Categoria, CategoriaResponseDTO>().ReverseMap();


        // Aluno
        CreateMap<AlunoRequestDTO, Aluno>();
        CreateMap<Aluno, AlunoResponseDTO>();
    }
}

// public class MappingProfile
// {
//    public static MapperConfiguration RegisterMaps()
//    {
//        var mappingConfig = new MapperConfiguration(config => {
//            CreateMap<Produto, ProdutoDTO>().ReverseMap();
//            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
//        });
//        return mappingConfig;
//    }
// }
