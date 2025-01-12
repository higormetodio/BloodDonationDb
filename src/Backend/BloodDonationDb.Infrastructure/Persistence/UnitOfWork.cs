using BloodDonationDb.Domain.SeedWorks;

namespace BloodDonationDb.Infrastructure.Persistence;
public class UnitOfWork : IUnitOfWork
{
    private readonly BloodDonationDbContext _dbContext;

    public UnitOfWork(BloodDonationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CommitAsync() => await _dbContext.SaveChangesAsync();
}
