namespace BloodDonationDb.Domain.Repositories.BloodStock;
public interface IBloodStockUpdateOnlyRepository
{
    void UpdateBloodStock(Entities.BloodStock bloodStock);
}
