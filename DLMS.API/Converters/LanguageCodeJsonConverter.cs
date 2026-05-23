using System.Text.Json;
using System.Text.Json.Serialization;
using DLMS.Domain.ValueObjects;

namespace DLMS.API.Converters;

public class LanguageCodeJsonConverter : JsonConverter<LanguageCode>
{
    public override LanguageCode Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var code = reader.GetString();
            return LanguageCode.Create(code ?? string.Empty);
        }

        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException("Language must be a string or an object with a code property.");

        string? languageCode = null;

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
                break;

            if (reader.TokenType != JsonTokenType.PropertyName)
                throw new JsonException("Invalid language object.");

            var propertyName = reader.GetString();
            reader.Read();

            if (string.Equals(propertyName, "code", StringComparison.OrdinalIgnoreCase))
                languageCode = reader.GetString();
            else
                reader.Skip();
        }

        return LanguageCode.Create(languageCode ?? string.Empty);
    }

    public override void Write(
        Utf8JsonWriter writer,
        LanguageCode value,
        JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("code", value.Code);
        writer.WriteEndObject();
    }
}
