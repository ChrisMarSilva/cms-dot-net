using AutoMapper;
using CMS_DotNet_Teste_Object_Mapping.Dtos;
using CMS_DotNet_Teste_Object_Mapping.Models;

namespace CMS_DotNet_Teste_Object_Mapping.Mappers;

public static class AutoMapperConfigurator
{
    public static IMapper AutoMapper { get; private set; }

    public static void SetUp()
    {
        AutoMapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Person, PersonDto>();

            //cfg.CreateMap<CustomerDto, Customer>();
            //cfg.CreateMap<OrderDto, Order>();
            //cfg.CreateMap<EmployeeDto, Employee>();
            //cfg.CreateMap<OrderDetailDto, OrderDetail>();
            //cfg.CreateMap<ShipperDto, Shipper>();
            //cfg.CreateMap<ProductDto, Product>();
            //cfg.CreateMap<CategoryDto, Category>();
            //cfg.CreateMap<SupplierDto, Supplier>();
            //cfg.CreateMap<CustomerDemographicDto, CustomerDemographic>();
            //cfg.CreateMap<TerritoryDto, Territory>();
            //cfg.CreateMap<RegionDto, Region>();
            //cfg.CreateMap<CityDto, City>();

            // cfg.CreateMap<SpotifyAlbumDto, SpotifyAlbum>();
            //            cfg.CreateMap<CopyrightDto, Copyright>();
            //            cfg.CreateMap<ArtistDto, Artist>();
            //            cfg.CreateMap<ExternalIdsDto, ExternalIds>();
            //            cfg.CreateMap<ExternalUrlsDto, ExternalUrls>();
            //            cfg.CreateMap<TracksDto, Tracks>();
            //            cfg.CreateMap<ImageDto, Image>();
            //            cfg.CreateMap<ItemDto, Item>();
            //            cfg.CreateMap<SpotifyAlbum, SpotifyAlbumDto>();
            //            cfg.CreateMap<Copyright, CopyrightDto>();
            //            cfg.CreateMap<Artist, ArtistDto>();
            //            cfg.CreateMap<ExternalIds, ExternalIdsDto>();
            //            cfg.CreateMap<ExternalUrls, ExternalUrlsDto>();
            //            cfg.CreateMap<Tracks, TracksDto>();
            //            cfg.CreateMap<Image, ImageDto>();
            //            cfg.CreateMap<Item, ItemDto>();

        }).CreateMapper();
    }
}
