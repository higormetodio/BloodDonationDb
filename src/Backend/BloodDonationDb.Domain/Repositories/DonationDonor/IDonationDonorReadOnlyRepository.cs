namespace BloodDonationDb.Domain.Repositories.DonationDonor;
public interface IDonationDonorReadOnlyRepository
{
    Task<IEnumerable<Entities.DonationDonor>> GetDonationDonorsByDate(DateTime startDate, DateTime finishDate);
}
