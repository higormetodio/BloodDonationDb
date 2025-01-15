namespace BloodDonationDb.Domain.Repositories.User;
public interface IUserReadOnlyRepository
{
    Task<Entities.User?> GetByEmailAsync(string email);
}
