using CMS_DotNet_Teste_Object_Mapping.Dtos;
using CMS_DotNet_Teste_Object_Mapping.Models;
using Nelibur.ObjectMapper;

namespace CMS_DotNet_Teste_Object_Mapping.Mappers;

public static class TinyMapperConfigurator
{
    public static void SetUp()
    {
        TinyMapper.Bind<PersonModel, PersonDto>();

        //TinyMapper.Bind<CustomerDto, Customer>();
        //TinyMapper.Bind<OrderDto, Order>();
        //TinyMapper.Bind<EmployeeDto, Employee>();
        //TinyMapper.Bind<OrderDetailDto, OrderDetail>();
        //TinyMapper.Bind<ShipperDto, Shipper>();
        //TinyMapper.Bind<ProductDto, Product>();
        //TinyMapper.Bind<CategoryDto, Category>();
        //TinyMapper.Bind<SupplierDto, Supplier>();
        //TinyMapper.Bind<CustomerDemographicDto, CustomerDemographic>();
        //TinyMapper.Bind<TerritoryDto, Territory>();
        //TinyMapper.Bind<RegionDto, Region>();
        //TinyMapper.Bind<List<CustomerDto>, List<Customer>>();
        //TinyMapper.Bind<CityDto, City>();
        //TinyMapper.Bind<List<CityDto>, List<City>>();

        //        Nelibur.ObjectMapper.TinyMapper.Bind<SpotifyAlbumDto, SpotifyAlbum>();
        //        Nelibur.ObjectMapper.TinyMapper.Bind<CopyrightDto, Copyright>();
        //        Nelibur.ObjectMapper.TinyMapper.Bind<ArtistDto, Artist>();
        //        Nelibur.ObjectMapper.TinyMapper.Bind<ExternalIdsDto, ExternalIds>();
        //        Nelibur.ObjectMapper.TinyMapper.Bind<ExternalUrlsDto, ExternalUrls>();
        //        Nelibur.ObjectMapper.TinyMapper.Bind<TracksDto, Tracks>();
        //        Nelibur.ObjectMapper.TinyMapper.Bind<ImageDto, Image>();
        //        Nelibur.ObjectMapper.TinyMapper.Bind<ItemDto, Item>();
        //        Nelibur.ObjectMapper.TinyMapper.Bind<SpotifyAlbum, SpotifyAlbumDto>();
        //        Nelibur.ObjectMapper.TinyMapper.Bind<Copyright, CopyrightDto>();
        //        Nelibur.ObjectMapper.TinyMapper.Bind<Artist, ArtistDto>();
        //        Nelibur.ObjectMapper.TinyMapper.Bind<ExternalIds, ExternalIdsDto>();
        //        Nelibur.ObjectMapper.TinyMapper.Bind<ExternalUrls, ExternalUrlsDto>();
        //        Nelibur.ObjectMapper.TinyMapper.Bind<Tracks, TracksDto>();
        //        Nelibur.ObjectMapper.TinyMapper.Bind<Image, ImageDto>();
        //        Nelibur.ObjectMapper.TinyMapper.Bind<Item, ItemDto>();
    }
}
