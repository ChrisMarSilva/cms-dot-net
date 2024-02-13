using System.Text.Json.Serialization;
using System.Text.Json;

namespace Rinha.Backend._2024.API.Models.Converters;

public class DateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string @string = reader.GetString();
        if (!string.IsNullOrEmpty(@string)) return DateTime.Parse(@string);
        return DateTime.MinValue;
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'ffffffZ"));
    }
}
