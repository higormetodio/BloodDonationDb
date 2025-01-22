using BloodDonationDb.Domain.Entities;
using BloodDonationDb.Domain.Repositories.Donor;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationDb.Infrastructure.Persistence.Repositories;
public class DonorRepository : IDonorWriteOnlyRepository, IDonorReadOnlyRepository
{
    private readonly BloodDonationDbContext _dbContext;

    public DonorRepository(BloodDonationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddDonorAsync(Donor donor) => await _dbContext.Donors.AddAsync(donor);

    public Task<bool> ExistActiveDonorWithEmail(string email) 
        => _dbContext.Donors.AnyAsync(donor => donor.Email!.Equals(email) && donor.Active);
    
}
