namespace BloodDonationDb.Domain.Repositories.User;
public interface IUserReadOnlyRepository
{
    Task<Entities.User?> GetByEmailAsync(string email);

    Task<bool> ExistsActiveUserWithEmail(string email);

    Task<bool> ExistsActiveUserWithIdentifier(Guid userIdentifier);
}
