using System.Text.Json.Serialization.Metadata;

namespace Project.Filters.Idempotency;

public interface IIdempotencySerializer
{
    string Serialize<T>(T instance, JsonTypeInfo<T> jsonTypeInfo);

    T Deserialize<T>(string json, JsonTypeInfo<T> jsonTypeInfo);
}