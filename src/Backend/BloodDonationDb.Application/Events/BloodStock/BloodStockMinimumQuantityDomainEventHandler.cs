using BloodDonationDb.Application.Abstraction;
using BloodDonationDb.Domain.Events;
using BloodDonationDb.Domain.Repositories.BloodStock;
using MongoDB.Driver;

namespace BloodDonationDb.Application.Events.BloodStock;
public class BloodStockMinimumQuantityDomainEventHandler
    : IDomainEventHanlder<BloodStockMinimumQuantityDomainEvent> 
{
    private readonly IBloodStockReadOnlyRepository _bloodStockReadOnlyRepository;
    private readonly IMongoCollection<Domain.Entities.BloodStock> _mongoCollection;

    public BloodStockMinimumQuantityDomainEventHandler(
        IBloodStockReadOnlyRepository bloodStockReadOnlyRepository, 
        IMongoCollection<Domain.Entities.BloodStock> mongoCollection)
    {
        _bloodStockReadOnlyRepository = bloodStockReadOnlyRepository;
        _mongoCollection = mongoCollection;
    }

    public async Task Handle(BloodStockMinimumQuantityDomainEvent notification, CancellationToken cancellationToken)
    {
        var bloodStock = await _bloodStockReadOnlyRepository.GetBloodStockByIdAsync(notification.BloodId);

        if (bloodStock is null)
        {
            return;
        }

        var result = await _mongoCollection.Find(b => b.Id == notification.BloodId).SingleOrDefaultAsync();

        if (result is not null)
        {
            return;
        }      

        await _mongoCollection.InsertOneAsync(bloodStock);
    }
}
