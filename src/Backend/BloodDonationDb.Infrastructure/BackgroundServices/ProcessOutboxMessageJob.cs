using BloodDonationDb.Comunication.Mediator;
using BloodDonationDb.Domain.Events;
using BloodDonationDb.Domain.SeedWorks;
using BloodDonationDb.Infrastructure.Converters;
using BloodDonationDb.Infrastructure.Persistence;
using BloodDonationDb.Infrastructure.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace BloodDonationDb.Infrastructure.BackgroundServices;

public class ProcessOutboxMessageJob : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ProcessOutboxMessageJob(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {            
            using var scope = _serviceScopeFactory.CreateAsyncScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<BloodDonationDbContext>();
            var publicsher = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();

            List<OutboxMessage> messages = await dbContext.Set<OutboxMessage>()
                                                          .Where(m => m.ProcessedOn == null)
                                                          .Take(20)
                                                          .ToListAsync(stoppingToken);          

            foreach (var outboxMessage in messages)
            {
                var settings = new JsonSerializerSettings();
                settings.Converters.Add(new DomainEventConverter());

                var domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(outboxMessage.Content, settings);

                if (domainEvent is null)
                {
                    continue;
                }

                await publicsher.PublishDomainEvent(domainEvent, stoppingToken);

                outboxMessage.ProcessedOn = DateTime.UtcNow;               
            }

            await dbContext.SaveChangesAsync();

            await Task.Delay(TimeSpan.FromSeconds(20), stoppingToken);
        }
    }
}
