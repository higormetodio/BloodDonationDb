using BloodDonationDb.Domain.Entities;
using BloodDonationDb.Domain.Repositories.DonationDonor;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationDb.Infrastructure.Persistence.Repositories;
public class DonationDonorRepository : IDonationDonorWriteOnlyRepository, IDonationDonorReadOnlyRepository
{
    private readonly BloodDonationDbContext _dbContext;

    public DonationDonorRepository(BloodDonationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<DonationDonor>> GetDonationDonorsByDate(DateTime startDate, DateTime finishDate)
        => await _dbContext.DonationDonor
            .AsNoTracking()
            .Include(d => d.Donor)
            .Include(b => b.BloodStock)
            .Where(donation => donation.When >= startDate && donation.When <= finishDate)
            .OrderBy(donation => donation.When.Date)
            .ToListAsync();

    public async Task AddDonationDonorAsync(DonationDonor donationDonor)
        => await _dbContext.DonationDonor.AddAsync(donationDonor);
}
