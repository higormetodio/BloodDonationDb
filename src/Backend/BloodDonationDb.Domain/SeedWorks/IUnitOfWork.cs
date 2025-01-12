namespace BloodDonationDb.Domain.SeedWorks;

public interface IUnitOfWork
{
    Task CommitAsync();
}   