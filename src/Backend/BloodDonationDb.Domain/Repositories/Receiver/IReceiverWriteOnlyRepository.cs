namespace BloodDonationDb.Domain.Repositories.Receiver;
public interface IReceiverWriteOnlyRepository
{
    Task AddReceiverAsync(Entities.Receiver receiver);
}
