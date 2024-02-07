using CMS_DotNet_Teste_Object_Mapping.Dtos;
using CMS_DotNet_Teste_Object_Mapping.Models;

namespace CMS_DotNet_Teste_Object_Mapping.Mappers;

public static class ExpressMapperConfigurator
{
    public static void SetUp()
    {
        ExpressMapper.Mapper.Register<Person, PersonDto>();

        //ExpressMapper.Mapper.Register<CustomerDto, Customer>();
        //ExpressMapper.Mapper.Register<OrderDto, Order>();
        //ExpressMapper.Mapper.Register<EmployeeDto, Employee>();
        //ExpressMapper.Mapper.Register<OrderDetailDto, OrderDetail>();
        //ExpressMapper.Mapper.Register<ShipperDto, Shipper>();
        //ExpressMapper.Mapper.Register<ProductDto, Product>();
        //ExpressMapper.Mapper.Register<CategoryDto, Category>();
        //ExpressMapper.Mapper.Register<SupplierDto, Supplier>();
        //ExpressMapper.Mapper.Register<CustomerDemographicDto, CustomerDemographic>();
        //ExpressMapper.Mapper.Register<TerritoryDto, Territory>();
        //ExpressMapper.Mapper.Register<RegionDto, Region>();
        //ExpressMapper.Mapper.Register<CityDto, City>();

        //        global::ExpressMapper.Mapper.Register<SpotifyAlbumDto, SpotifyAlbum>();
        //        global::ExpressMapper.Mapper.Register<CopyrightDto, Copyright>();
        //        global::ExpressMapper.Mapper.Register<ArtistDto, Artist>();
        //        global::ExpressMapper.Mapper.Register<ExternalIdsDto, ExternalIds>();
        //        global::ExpressMapper.Mapper.Register<ExternalUrlsDto, ExternalUrls>();
        //        global::ExpressMapper.Mapper.Register<TracksDto, Tracks>();
        //        global::ExpressMapper.Mapper.Register<ImageDto, Image>();
        //        global::ExpressMapper.Mapper.Register<ItemDto, Item>();
        //        global::ExpressMapper.Mapper.Register<SpotifyAlbum, SpotifyAlbumDto>();
        //        global::ExpressMapper.Mapper.Register<Copyright, CopyrightDto>();
        //        global::ExpressMapper.Mapper.Register<Artist, ArtistDto>();
        //        global::ExpressMapper.Mapper.Register<ExternalIds, ExternalIdsDto>();
        //        global::ExpressMapper.Mapper.Register<ExternalUrls, ExternalUrlsDto>();
        //        global::ExpressMapper.Mapper.Register<Tracks, TracksDto>();
        //        global::ExpressMapper.Mapper.Register<Image, ImageDto>();
        //        global::ExpressMapper.Mapper.Register<Item, ItemDto>();
    }
}
