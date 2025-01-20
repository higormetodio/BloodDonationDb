using BloodDonationDb.Domain.Repositories.User;
using BloodDonationDb.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationDb.Infrastructure.Persistence.Repositories;
public class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository
{
    private readonly BloodDonationDbContext _dbContext;

    public UserRepository(BloodDonationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddUserAsync(User user) => await _dbContext.Users.AddAsync(user);

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Active && user.Email.Equals(email));
    }

    public async Task<bool> ExistsActiveUserWithEmail(string email)
    {
        return await _dbContext.Users.AnyAsync(u => u.Email.Equals(email) && u.Active);
    }

    public async Task<bool> ExistsActiveUserWithIdentifier(Guid userIdentifier)
        => await _dbContext.Users.AnyAsync(user => user.Id.Equals(userIdentifier) && user.Active);
}
