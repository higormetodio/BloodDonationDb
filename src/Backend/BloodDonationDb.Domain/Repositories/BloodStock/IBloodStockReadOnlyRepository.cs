using BloodDonationDb.Domain.Enums;

namespace BloodDonationDb.Domain.Repositories.BloodStock;

public interface IBloodStockReadOnlyRepository
{
    Task<IEnumerable<Entities.BloodStock>> GetAllBloodStocksAsync();
    Task<Entities.BloodStock> GetBloodStockAsync(BloodType bloodType, RhFactor rhFactor);

    Task<Entities.BloodStock> GetBloodStockByIdAsync(Guid id);
}