using BloodDonationDb.Domain.SeedWorks;

namespace BloodDonationDb.Domain.Repositories.Donor;

public interface IDonorWriteOnlyRepository
{
   Task AddDonorAsync(Entities.Donor donor);
}