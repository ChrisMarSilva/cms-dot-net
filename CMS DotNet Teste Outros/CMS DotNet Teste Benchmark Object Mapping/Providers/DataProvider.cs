using CMS_DotNet_Teste_Object_Mapping.Dtos;
using CMS_DotNet_Teste_Object_Mapping.Models;
using Tynamix.ObjectFiller;

namespace CMS_DotNet_Teste_Object_Mapping.Providers;

public static class DataProvider
{
    //public static List<CustomerDto> GetDataCustomerDtos(int count)
    //{
    //    Filler<CustomerDto> filler = new Filler<CustomerDto>();
    //    filler.Setup();
    //    return filler.Create(count).ToList();
    //}

    //public static List<CityDto> GetDataAddresseDtos(int count)
    //{
    //    Filler<CityDto> filler = new Filler<CityDto>();
    //    filler.Setup();
    //    return filler.Create(count).ToList();
    //}

    public static Person GetDataPerson()
    {
        var filler = new Filler<Person>();
        filler.Setup();
        return filler.Create();
        // return new Person { Id = 1, FirstName = "John", LastName = "Doe", Birthday = DateTime.UtcNow };
    }

    public static List<Person> GetDataPersons(int count)
    {
        var filler = new Filler<Person>();
        filler.Setup();
        return filler.Create(count).ToList();
    }
}
