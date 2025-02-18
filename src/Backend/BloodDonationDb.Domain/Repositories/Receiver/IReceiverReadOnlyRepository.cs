namespace BloodDonationDb.Domain.Repositories.Receiver;
public interface IReceiverReadOnlyRepository
{
    Task<Entities.Receiver> GetReceiverByEmail(string email);
}
