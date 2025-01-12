using BloodDonationDb.Domain.SeedWorks;

namespace BloodDonationDb.Domain.Entities;

public class User : Entity, IAggregateRoot
{
    public User(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
        Active = true;  
    }

    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public bool Active { get; set; }
}
