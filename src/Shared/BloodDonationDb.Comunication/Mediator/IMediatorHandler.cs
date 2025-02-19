using BloodDonationDb.Domain.SeedWorks;

namespace BloodDonationDb.Comunication.Mediator;
public interface IMediatorHandler
{
    Task PublishDomainEvent<TEvent>(TEvent domainEvent, CancellationToken cancellationToken) where TEvent : IDomainEvent;
}
