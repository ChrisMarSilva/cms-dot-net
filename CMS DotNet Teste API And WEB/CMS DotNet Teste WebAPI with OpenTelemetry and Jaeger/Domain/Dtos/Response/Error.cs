using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Project.Domain.Dtos.Response;

public class Error
{
    private readonly ErrorResponseDto _parent;

    [JsonPropertyName("Campo")]
    public string Field { get; init; }

    [JsonPropertyName("Descricoes")]
    public IList<string> Descriptions { get; init; }

    [JsonConstructor]
    public Error()
    {
    }

    public Error(string field, ErrorResponseDto parent)
    {
        _parent = parent;
        Field = field;
        Descriptions = new List<string>();
    }

    public Error AddDescription(string description)
    {
        Descriptions.Add(description);
        return this;
    }

    public Error ThenAddError(string field)
    {
        return _parent.AddError(field);
    }

    public ErrorResponseDto End()
    {
        return _parent;
    }
}