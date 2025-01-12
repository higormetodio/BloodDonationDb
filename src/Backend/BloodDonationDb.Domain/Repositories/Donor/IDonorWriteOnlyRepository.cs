using BloodDonationDb.Domain.SeedWorks;

namespace BloodDonationDb.Domain.Repositories.Donor;

public interface IDonorWriteOnlyRepository
{
   public Task AddDonorAsync(Entities.Donor donor);
}