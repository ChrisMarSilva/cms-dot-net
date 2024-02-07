using CMS_DotNet_Teste_Object_Mapping.Dtos;
using CMS_DotNet_Teste_Object_Mapping.Models;
using Nelibur.ObjectMapper;

namespace CMS_DotNet_Teste_Object_Mapping.Mappers;

public static class TinyMapperConfigurator
{
    public static void SetUp()
    {
        TinyMapper.Bind<Person, PersonDto>();
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
    }
}
