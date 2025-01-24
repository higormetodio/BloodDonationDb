namespace BloodDonationDb.Domain.Repositories.Donor;

public interface IDonorReadOnlyRepository
{
    public Task<Entities.Donor> GetDonorByEmailAsync(string email); 
    public Task<bool> ExistActiveDonorWithEmail(string email);
}