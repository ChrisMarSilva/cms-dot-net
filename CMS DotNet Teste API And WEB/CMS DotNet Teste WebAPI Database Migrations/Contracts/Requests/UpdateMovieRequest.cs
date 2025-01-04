namespace CMS_DotNet_Teste_WebAPI_Database_Migrations.Contracts.Requests;

public class UpdateMovieRequest
{
    public required string Title { get; init; }
    public required int YearOfRelease { get; init; }
}
