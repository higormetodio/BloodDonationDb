using BloodDonationDb.Domain.SeedWorks;

namespace BloodDonationDb.Domain.Repositories.User;
public interface IUserWriteOnlyRepository
{
    Task AddUserAsync(Entities.User user);
}
