namespace CMS_DotNet_Teste_WebAPI_Database_Migrations.Contracts.Responses;

public class MoviesResponse
{
    public required IEnumerable<MovieResponse> Items { get; init; } = Enumerable.Empty<MovieResponse>();
}
