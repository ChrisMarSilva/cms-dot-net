using CMS_DotNet_Teste_Object_Mapping.Models;
using Tynamix.ObjectFiller;

namespace CMS_DotNet_Teste_Object_Mapping.Providers;

public static class DataProviderTynamix
{
    private static readonly Filler<PersonModel> _fillerPerson;

    static DataProviderTynamix()
    {
        _fillerPerson = new Filler<PersonModel>();

        _fillerPerson
            //.SetRandomSeed(1234)
            .Setup()
            .OnProperty(x => x.Id).Use(new IntRange(1, 1_000_000)) // Enumerable.Range(1, 1_000_000)
            .OnProperty(x => x.FirstName).Use(new RealNames(NameStyle.FirstName)) // .Use("John")// new MnemonicString(1) // .Use(new MnemonicString(2, 5))
            .OnProperty(x => x.LastName).Use(new RealNames(NameStyle.LastName)) // .Use(new MnemonicString(2, 5))
            .OnProperty(x => x.Birthday).Use(new DateTimeRange(DateTime.Now.AddYears(-50), DateTime.Now.AddYears(-18)));
    }

    public static PersonModel GetDataPerson() =>
         _fillerPerson.Create();

    public static IEnumerable<PersonModel> GetDataPersons(int count) =>
         _fillerPerson.Create(count).ToList();
}
