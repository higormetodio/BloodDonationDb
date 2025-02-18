using BloodDonationDb.Domain.Entities;
using BloodDonationDb.Domain.Repositories.Receiver;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationDb.Infrastructure.Persistence.Repositories;
public class ReceiverRepository : IReceiverWriteOnlyRepository, IReceiverReadOnlyRepository
{
    private readonly BloodDonationDbContext _context;

    public ReceiverRepository(BloodDonationDbContext context)
    {
        _context = context;
    }

    public async Task AddReceiverAsync(Receiver receiver)
        => await _context.Receivers.AddAsync(receiver);

    public async Task<Receiver> GetReceiverByEmail(string email)
        => await _context.Receivers
        .AsNoTracking()
        .SingleOrDefaultAsync(receiver => receiver.Email.Equals(email) && receiver.Active);
}
