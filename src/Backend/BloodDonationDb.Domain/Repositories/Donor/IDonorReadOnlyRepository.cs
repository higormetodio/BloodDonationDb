using BloodDonationDb.Domain.SeedWorks;

namespace BloodDonationDb.Domain.Repositories.Donor;

public interface IDonorReadOnlyRepository
{
    Task<bool> ExistActiveDonorWithEmail(string email);
}