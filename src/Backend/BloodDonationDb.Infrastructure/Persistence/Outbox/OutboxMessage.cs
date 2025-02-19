namespace BloodDonationDb.Infrastructure.Persistence.Outbox;
public sealed class OutboxMessage
{
    public Guid Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime OccurredOn { get; set; }
    public DateTime? ProcessedOn { get; set; } = null;
    public string? Error { get; set; } = null;
}
