namespace CMS_DotNet_Teste_WebAPI_Database_Migrations.Contracts.Responses;

public class MovieResponse
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required int YearOfRelease { get; init; }
}