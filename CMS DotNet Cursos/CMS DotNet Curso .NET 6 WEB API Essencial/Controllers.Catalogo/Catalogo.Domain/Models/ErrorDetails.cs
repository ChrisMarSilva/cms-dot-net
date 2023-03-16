using System.Text.Json;

namespace Catalogo.Domain.Models;

public class ErrorDetails
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public string Trace { get; set; } = string.Empty;

    // public override string ToString() => JsonConvert.SerializeObject(this);
    public override string ToString() => JsonSerializer.Serialize(this);
}
