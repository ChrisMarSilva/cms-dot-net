using Microsoft.Extensions.Options;

namespace Project.Filters.Idempotency;

public class HttpRequestIdempotencyKeyReader : IIdempotencyKeyReader<HttpRequest>
{
    private readonly IdempotencyOptions _options;

    public HttpRequestIdempotencyKeyReader(IOptions<IdempotencyOptions> options)
    {
        _options = options.Value;
    }

    public string Read(HttpRequest request)
    {
        var headerKey = request.Headers.Keys.FirstOrDefault(key => key.Equals(_options.HeaderName, StringComparison.InvariantCultureIgnoreCase));

        if (headerKey is null || string.IsNullOrWhiteSpace(request.Headers[headerKey]))
            return null;

        return $"[{request.Method}] {request.Path} - {request.Headers[headerKey]}";
    }
}