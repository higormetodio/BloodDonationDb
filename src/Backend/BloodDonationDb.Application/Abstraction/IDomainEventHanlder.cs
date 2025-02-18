using BloodDonationDb.Domain.SeedWorks;
using MediatR;

namespace BloodDonationDb.Application.Abstraction;
public interface IDomainEventHanlder<TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
{
}
