namespace BloodDonationDb.Domain.Repositories.DonationDonor;
public interface IDonationDonorWriteOnlyRepository
{
    Task AddDonationDonorAsync(Entities.DonationDonor donationDonor);
}
