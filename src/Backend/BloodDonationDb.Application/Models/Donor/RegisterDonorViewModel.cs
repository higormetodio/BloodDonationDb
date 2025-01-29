using BloodDonationDb.Domain.Entities;
using BloodDonationDb.Domain.Enums;
using MongoDB.Driver;

namespace BloodDonationDb.Application.Models.Donor;
public class RegisterDonorViewModel
{
    public RegisterDonorViewModel(Guid donorID, string? name)
    {
        DonorId = donorID;
        Name = name;
    }

    public Guid DonorId { get; private set; }
    public string? Name { get; private set; }

    public static RegisterDonorViewModel FromEntity(Domain.Entities.Donor entity)
        => new(entity.Id, entity.Name);
}
