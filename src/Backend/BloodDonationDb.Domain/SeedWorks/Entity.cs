using MediatR;

namespace BloodDonationDb.Domain.SeedWorks;

public abstract class Entity
{
    protected Entity()
    {
        Id = Guid.NewGuid();
        CreateOn = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }
    public DateTime CreateOn { get; private set; }
    
    private List<INotification>? _domainEvents;
    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly()!;

    public void AddDomainEvent(INotification eventItem)
    {
        _domainEvents ??= [];
        _domainEvents.Add(eventItem);
    }

    public void RemoveDomainEvent(INotification eventItem)
    {
        _domainEvents?.Remove(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }
}