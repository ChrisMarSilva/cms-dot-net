using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Security.Cryptography;
using System.Text.Json.Serialization;

namespace Project.Filters.Idempotency;

public class IdempotencyRegister : IIdempotencyRegister
{
    [ExcludeFromCodeCoverage]
    public IdempotencyRegister() { }

    public string Key { get; set; }

    public bool IsCompleted { get; set; }

    public IReadOnlyList<byte> Value { get; set; }

    public int? StatusCode { get; set; }

    public string ContentType { get; set; }

    public IReadOnlyList<byte> HashOfRequest { get; set; }

    public static async Task<byte[]> ComputeHash(Stream data)
    {
        await using var buffer = MemoryStreamExtension.RecyclableMemoryStreamManager.GetStream();

        if (data.CanSeek)
            data.Seek(0, SeekOrigin.Begin);

        await data.CopyToAsync(buffer);
        buffer.Seek(0, SeekOrigin.Begin);

        if (data.CanSeek)
            data.Seek(0, SeekOrigin.Begin);

        using var provider = MD5.Create();

        return await provider.ComputeHashAsync(buffer);
    }

    public static async Task<IdempotencyRegister> Of(string key, int statusCode, string contentType, Stream request, Stream response)
    {
        static void CheckParameters(string key, int statusCode, Stream request, Stream response)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            if (statusCode >= (int)HttpStatusCode.BadRequest)
                throw new ArgumentException("The status code should be a success.", nameof(statusCode));

            if (request is null)
                throw new ArgumentNullException(nameof(request));

            if (response is null)
                throw new ArgumentNullException(nameof(response));
        }

        CheckParameters(key, statusCode, request, response);

        await using var stream = MemoryStreamExtension.RecyclableMemoryStreamManager.GetStream();

        response.Seek(0, SeekOrigin.Begin);
        await response.CopyToAsync(stream);
        response.Seek(0, SeekOrigin.Begin);
        stream.Seek(0, SeekOrigin.Begin);

        return new IdempotencyRegister
        {
            ContentType = contentType,
            IsCompleted = true,
            Key = key,
            StatusCode = statusCode,
            Value = new List<byte>(stream.ToArray()),
            HashOfRequest = new List<byte>(await ComputeHash(request))
        };
    }

    public static IdempotencyRegister Of(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentNullException(nameof(key));

        return new IdempotencyRegister
        {
            Key = key,
            Value = null,
            ContentType = null,
            IsCompleted = false,
            StatusCode = null,
            HashOfRequest = null
        };
    }
}

[JsonSerializable(typeof(IdempotencyRegister))]
public partial class IdempotencyRegisterCustomContext : JsonSerializerContext { }