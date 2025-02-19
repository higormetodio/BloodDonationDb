using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BloodDonationDb.Domain.SeedWorks;

public abstract class Entity
{
    protected Entity()
    {
        Id = Guid.NewGuid();
        CreateOn = DateTime.UtcNow;
    }

    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; private set; }
    public DateTime CreateOn { get; private set; }    
}