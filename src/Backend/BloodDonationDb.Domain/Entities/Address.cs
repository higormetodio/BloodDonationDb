namespace BloodDonationDb.Domain.Entities;

public class Address : BaseEntity
{
    public Address(int donorId, string street, string city, string state, string zipCode)
    {
        DonorId = donorId;
        Street = street;
        City = city;
        State = state;
        ZipCode = zipCode;
    }

    public int DonorId { get; private set; }
    public Donor Donor { get; private set; }
    public string Street { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string ZipCode { get; private set; }

    public void UpdateAddress(string street, string city, string state, string zipCode)
    {
        Street = street;
        City = city;
        State = state;
        ZipCode = zipCode;
        
    }
}