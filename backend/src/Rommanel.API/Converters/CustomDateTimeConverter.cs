using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rommanel.API.Converters;

public class CustomDateTimeConverter : JsonConverter<DateTime>
{
    private readonly string[] formats =
    [
        "dd/MM/yyyy",
        "yyyy-MM-dd"
    ];

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString() ?? "";

        if (DateTime.TryParseExact(value, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
        {
            var dateOfBirth = DateTime.SpecifyKind(result, DateTimeKind.Utc);
            return dateOfBirth;
        }
        throw new JsonException($"Formato de data inválido. Aceitos: {string.Join(", ", formats)}");
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(formats[0]));
    }
}
