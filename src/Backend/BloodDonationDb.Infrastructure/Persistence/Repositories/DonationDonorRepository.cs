using BloodDonationDb.Domain.Entities;
using BloodDonationDb.Domain.Repositories.DonationDonor;

namespace BloodDonationDb.Infrastructure.Persistence.Repositories;
public class DonationDonorRepository : IDonationDonorWriteOnlyRepository
{
    private readonly BloodDonationDbContext _dbContext;

    public DonationDonorRepository(BloodDonationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddDonationDonorAsync(DonationDonor donationDonor)
        => await _dbContext.DonationDonor.AddAsync(donationDonor);
}
