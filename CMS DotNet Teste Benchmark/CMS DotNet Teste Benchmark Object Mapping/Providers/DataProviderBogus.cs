using Bogus;
using CMS_DotNet_Teste_Object_Mapping.Models;

namespace CMS_DotNet_Teste_Object_Mapping.Providers;

public static class DataProviderBogus
{
    private static readonly Faker<PersonModel> _fakerPerson = 
        new Faker<PersonModel>("pt_BR")
            .RuleFor(x => x.Id, f => f.IndexFaker)
            .RuleFor(x => x.FirstName, f => f.Name.FirstName())
            .RuleFor(x => x.LastName, f => f.Name.LastName())
            .RuleFor(x => x.Birthday, f => f.Date.Past(30));

    public static PersonModel GetDataPerson() => 
        _fakerPerson.Generate();

    public static ICollection<PersonModel> GetDataPersons(int count) => 
        _fakerPerson.Generate(count);
}
