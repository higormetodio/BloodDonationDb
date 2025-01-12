using BloodDonationDb.Domain.SeedWorks;

namespace BloodDonationDb.Domain.Repositories.Donor;

public interface IDonorReadOnlyRepository
{
    Task<Entities.Donor> GetDonorByEmailAsync(string email);
}