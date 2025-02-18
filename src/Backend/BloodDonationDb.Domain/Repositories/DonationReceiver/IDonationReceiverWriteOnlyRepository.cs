namespace BloodDonationDb.Domain.Repositories.DonationReceiver;
public interface IDonationReceiverWriteOnlyRepository
{
    Task AddDonationReceiverAsync(Entities.DonationReceiver donationReceiver);
}
