using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace BloodDonationDb.API.Converters;

public partial class StringConverter : JsonConverter<string>
{
    public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString()?.Trim();

        return value is null ? null : RemoveExtraWiteSpace().Replace(value, " ");
    }    

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options) => writer.WriteStringValue(value);


    [GeneratedRegex(@"\s+")]
    private static partial Regex RemoveExtraWiteSpace();
}
