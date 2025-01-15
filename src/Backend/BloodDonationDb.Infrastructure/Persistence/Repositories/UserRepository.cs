using BloodDonationDb.Domain.Repositories.User;
using BloodDonationDb.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationDb.Infrastructure.Persistence.Repositories;
public class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository
{
    private readonly BloodDonationDbContext _context;

    public UserRepository(BloodDonationDbContext context)
    {
        _context = context;
    }

    public async Task AddUserAsync(User user) => await _context.Users.AddAsync(user);

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Active && user.Email.Equals(email));
    }
}
