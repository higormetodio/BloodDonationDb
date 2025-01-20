using BloodDonationDb.Domain.Entities;

namespace BloodDonationDb.Domain.Services.LoggedUser;
public interface ILoggedUser
{
    public Task<User> User();
}
