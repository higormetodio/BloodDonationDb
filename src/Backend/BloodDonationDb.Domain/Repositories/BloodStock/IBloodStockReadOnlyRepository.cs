using BloodDonationDb.Domain.Entities;
using BloodDonationDb.Domain.SeedWorks;

namespace BloodDonationDb.Domain.Repositories.BloodStock;

public interface IBloodStockReadOnlyRepository
{
    Task<Entities.BloodStock> GetAllBloodStockAsync();
}