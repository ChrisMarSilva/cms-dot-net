using System.Text.Json.Serialization.Metadata;
using System.Text.Json;

namespace Project.Filters.Idempotency;

public class IdempotencySerializer : IIdempotencySerializer
{
    public T Deserialize<T>(string json, JsonTypeInfo<T> jsonTypeInfo) => JsonSerializer.Deserialize(json, jsonTypeInfo);

    public string Serialize<T>(T instance, JsonTypeInfo<T> jsonTypeInfo) => JsonSerializer.Serialize(instance, jsonTypeInfo);
}