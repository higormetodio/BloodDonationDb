using BloodDonationDb.Domain.Repositories.User;
using BloodDonationDb.Domain.Entities;

namespace BloodDonationDb.Infrastructure.Persistence.Repositories;
public class UserRepository : IUserWriteOnlyRepository
{
    private readonly BloodDonationDbContext _context;

    public UserRepository(BloodDonationDbContext context)
    {
        _context = context;
    }

    public async Task AddUserAsync(User user) => await _context.Users.AddAsync(user);

}
