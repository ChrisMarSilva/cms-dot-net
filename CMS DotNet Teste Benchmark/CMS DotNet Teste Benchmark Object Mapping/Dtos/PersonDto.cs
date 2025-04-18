using CMS_DotNet_Teste_Object_Mapping.Models;

namespace CMS_DotNet_Teste_Object_Mapping.Dtos;

public record PersonDto
{
    public static implicit operator PersonDto(PersonModel person) =>
        new PersonDto { Id = person.Id, FirstName = person.FirstName, LastName = person.LastName, Birthday = person.Birthday };

    //public PersonDto(int id, string firstName, string lastName, DateTime birthday)
    //{
    //    Id = id;
    //    FirstName = firstName;
    //    LastName = lastName;
    //    Birthday = birthday;
    //}

    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime Birthday { get; set; }

    //public override string ToString() => 
    //    $"PersonDto({Id}: {FirstName} {LastName} - {Birthday:dd/MM/yyyy})";
}
