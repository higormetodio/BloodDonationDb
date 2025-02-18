using BloodDonationDb.Domain.Enums;

namespace BloodDonationDb.Domain.Repositories.BloodStock;

public interface IBloodStockReadOnlyRepository
{
    Task<Entities.BloodStock> GetBloodStockAsync(BloodType bloodType, RhFactor rhFactor);

    Task<Entities.BloodStock> GetBloodStockByIdAsync(Guid id);
}