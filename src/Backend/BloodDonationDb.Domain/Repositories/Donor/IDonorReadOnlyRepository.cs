namespace BloodDonationDb.Domain.Repositories.Donor;

public interface IDonorReadOnlyRepository
{
    Task<Entities.Donor> GetDonorByEmailAsync(string email); 

    Task<bool> ExistActiveDonorWithEmail(string email);
}