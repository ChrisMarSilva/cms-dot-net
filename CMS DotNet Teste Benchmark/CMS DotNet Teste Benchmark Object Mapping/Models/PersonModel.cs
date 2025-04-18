namespace CMS_DotNet_Teste_Object_Mapping.Models;

public sealed class PersonModel
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime Birthday { get; set; }
}
