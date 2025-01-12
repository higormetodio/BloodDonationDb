using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BloodDonationDb.Infrastructure.Persistence.Outbox;
public sealed class OutboxMessage
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime OccurredOn { get; set; }
    public DateTime? ProcessedOn { get; set; }
    public string? Error { get; set; }
}
