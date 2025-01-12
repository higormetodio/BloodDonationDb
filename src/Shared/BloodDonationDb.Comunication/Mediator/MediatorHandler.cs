using BloodDonationDb.Domain.SeedWorks;
using MediatR;

namespace BloodDonationDb.Comunication.Mediator;
public class MediatorHandler : IMediatorHandler
{
    private readonly IMediator _mediator;

    public MediatorHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task PublishDomainEvent<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent
    {
        return _mediator.Publish(domainEvent);
    }
}
