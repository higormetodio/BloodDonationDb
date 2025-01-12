//using BloodDonationDb.Domain.SeedWorks;
//using BloodDonationDb.Infrastructure.Persistence.Outbox;
//using MediatR.Pipeline;
//using MongoDB.Driver;
//using System.Text.Json;

//namespace BloodDonationDb.Infrastructure.Interceptors;
//public class PublishDomainEventsToOutBoxMessagesInterceptor : IRequestPreProcessor<IDomainEvent>
//{
//    private readonly IMongoCollection<OutboxMessage> _collections;

//    public PublishDomainEventsToOutBoxMessagesInterceptor(IMongoDatabase mongoDatabase)
//    {
//        _collections = mongoDatabase.GetCollection<OutboxMessage>(nameof(OutboxMessage));
//    }
//    public async Task Process(IDomainEvent domainEvent, CancellationToken cancellationToken)
//    {
//        var outboxMessage = new OutboxMessage
//        {
//            Id = Guid.NewGuid(),
//            Type = domainEvent.GetType().Name,
//            Content = JsonSerializer.Serialize(domainEvent),
//            OccurredOn = DateTime.UtcNow
//        };

//        await _collections.InsertOneAsync(outboxMessage, cancellationToken: cancellationToken);
//    }
//}
