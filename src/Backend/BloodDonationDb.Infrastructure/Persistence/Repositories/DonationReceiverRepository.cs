using BloodDonationDb.Domain.Entities;
using BloodDonationDb.Domain.Repositories.DonationReceiver;

namespace BloodDonationDb.Infrastructure.Persistence.Repositories;
public class DonationReceiverRepository : IDonationReceiverWriteOnlyRepository
{
    private readonly BloodDonationDbContext _context;

    public DonationReceiverRepository(BloodDonationDbContext context)
    {
        _context = context;
    }

    public async Task AddDonationReceiverAsync(DonationReceiver donationReceiver)
        => await _context.DonationReceiver.AddAsync(donationReceiver);
}
