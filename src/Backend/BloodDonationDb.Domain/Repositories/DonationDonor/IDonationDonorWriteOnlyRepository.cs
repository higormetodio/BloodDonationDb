namespace BloodDonationDb.Domain.Repositories.DonationDonor;
public interface IDonationDonorWriteOnlyRepository
{
    public Task AddDonationDonorAsync(Entities.DonationDonor donationDonor);
}
