using BloodDonationDb.Domain.SeedWorks;

namespace BloodDonationDb.Domain.Entities;
public class RefreshToken : Entity
{
    public RefreshToken(string value, Guid userId)
    {
        Value = value;
        UserId = userId;
    }

    protected RefreshToken() { }

    public string? Value { get; private set; }
    public Guid UserId { get; set; }
    public User? User { get; private set; }
}
