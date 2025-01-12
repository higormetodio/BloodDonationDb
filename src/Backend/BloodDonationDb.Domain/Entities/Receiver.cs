using BloodDonationDb.Domain.SeedWorks;
using BloodDonationDb.Domain.ValueObjects;

namespace BloodDonationDb.Domain.Entities;

public class Receiver : Entity, IAggregateRoot
{
    public Receiver(string name, string email, Address address)
    {
        Name = name;
        Email = email;
        Address = address;
        Active  = true;
    }
    
    protected Receiver(){ }

    public string Name { get; private set; }
    public string Email { get; private set; }
    public Address Address { get; private set; }
    public bool Active { get; private set; }

    public IEnumerable<DonationReceiver>? Donations { get; private set; }
}