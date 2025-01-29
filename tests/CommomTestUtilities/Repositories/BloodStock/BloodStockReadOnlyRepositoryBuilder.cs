using BloodDonationDb.Domain.Repositories.BloodStock;
using NSubstitute;

namespace CommomTestUtilities.Repositories.BloodStock;
public class BloodStockReadOnlyRepositoryBuilder
{
    private readonly IBloodStockReadOnlyRepository _repository = Substitute.For<IBloodStockReadOnlyRepository>();

    public void GetBloodStock(BloodDonationDb.Domain.Entities.BloodStock bloodStock)
    {
        _repository.GetBloodStockAsync(bloodStock.BloodType, bloodStock.RhFactor).Returns(Task.FromResult(bloodStock));
    }

    public IBloodStockReadOnlyRepository Builder() => _repository;
}
