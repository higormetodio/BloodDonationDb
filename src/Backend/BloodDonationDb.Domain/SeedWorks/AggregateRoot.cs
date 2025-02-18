namespace BloodDonationDb.Domain.SeedWorks;

public abstract class AggregateRoot : Entity
{
    private List<IDomainEvent>? _domainEvents = [];

    protected AggregateRoot()
    {        
    }

    public IReadOnlyCollection<IDomainEvent> GetDomainEvents() => _domainEvents!.ToList();

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents ??= [];
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }
}