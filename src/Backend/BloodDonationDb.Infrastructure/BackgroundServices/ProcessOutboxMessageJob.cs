//using BloodDonationDb.Comunication.Mediator;
//using BloodDonationDb.Domain.SeedWorks;
//using BloodDonationDb.Infrastructure.Persistence.Outbox;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using MongoDB.Driver;
//using System.Text.Json;

//namespace BloodDonationDb.Infrastructure.BackgroundServices;

//public class ProcessOutboxMessageJob : BackgroundService
//{
//    private readonly IMongoCollection<OutboxMessage> _collection;
//    private readonly IMediatorHandler _mediatorHandler;
//    private readonly IServiceScopeFactory _serviceScopeFactory;

//    public ProcessOutboxMessageJob(IMongoDatabase mongoDatabase, IMediatorHandler mediatorHandler, IServiceScopeFactory serviceScopeFactory)
//    {
//        _collection = mongoDatabase.GetCollection<OutboxMessage>(nameof(OutboxMessage));
//        _mediatorHandler = mediatorHandler;
//        _serviceScopeFactory = serviceScopeFactory;
//    }
//    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//    {
//        while (!stoppingToken.IsCancellationRequested)
//        {
//            var filter = Builders<OutboxMessage>.Filter.Eq(o => o.ProcessedOn, DateTime.MinValue);

//            var messages = await _collection.Find(filter).ToListAsync();

//            foreach (var outboxMessage in messages)
//            {
//                IDomainEvent? domainEvent = JsonSerializer
//                    .Deserialize<IDomainEvent>(outboxMessage.Content);

//                if (domainEvent is null)
//                {
//                    continue;
//                }

//                await _mediatorHandler.PublishDomainEvent(domainEvent);

//                outboxMessage.ProcessedOn = DateTime.UtcNow;

//                var update = Builders<OutboxMessage>.Update
//                    .Set(o => o.ProcessedOn, outboxMessage.ProcessedOn);

//                _collection.UpdateOne(filter, update);
//            }

//            await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken);
//        }        
//    }
//}
