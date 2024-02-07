using CMS_DotNet_Teste_Object_Mapping.Models;
using System.Xml.Linq;

namespace CMS_DotNet_Teste_Object_Mapping.Dtos;

public class PersonDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthday { get; set; }

    public static implicit operator PersonDto(Person person)
    {
        return new PersonDto
        {
            Id = person.Id,
            FirstName = person.FirstName,
            LastName = person.LastName,
            Birthday = person.Birthday
        };
    }

    public override string ToString()
    {
        return $"PersonDto( " +
            $"Id: {Id}" +
            $" - FirstName: {FirstName}" +
            $" - LastName: {LastName}" +
            $" - Birthday: {Birthday:dd/MM/yyyy} )";
    }
}
