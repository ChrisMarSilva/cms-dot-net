using System.ComponentModel.DataAnnotations;

namespace CMS_DotNet_Teste_WebAPI_Database_Migrations.Movies;

public class Movie
{
    [Key]
    public required Guid Id { get; init; }
    public required string Title { get; set; }
    public required int YearOfRelease { get; set; }
}
