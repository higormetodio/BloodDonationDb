using BloodDonationDb.Domain.SeedWorks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BloodDonationDb.Infrastructure.Converters;
public class DomainEventConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return (objectType == typeof(IDomainEvent));
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        var jsonObject = JObject.Load(reader);
        
        var eventType = jsonObject["$type"]!.Value<string>();
        
        var type = Type.GetType(eventType!);

        return serializer.Deserialize(jsonObject.CreateReader(), type);
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        serializer.Serialize(writer, value);
    }
}
