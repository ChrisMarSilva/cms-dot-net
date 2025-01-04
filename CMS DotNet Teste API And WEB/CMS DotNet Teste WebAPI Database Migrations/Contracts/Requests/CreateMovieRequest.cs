namespace CMS_DotNet_Teste_WebAPI_Database_Migrations.Contracts.Requests;

public class CreateMovieRequest
{
    public required string Title { get; init; }
    public required int YearOfRelease { get; init; }
}
