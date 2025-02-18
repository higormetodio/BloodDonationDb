using BloodDonationDb.Application.Models.DonationReceiver;
using BloodDonationDb.Domain.Entities;
using BloodDonationDb.Domain.Enums;
using MediatR;

namespace BloodDonationDb.Application.Commands.DonationReceiver.Register;
public class RegisterDonationReceiverCommand : IRequest<RegisterDonationReceiverViewModel>
{
    public string? Email { get; set; }
    public DateTime DonationDate { get; set; }
    public BloodType BloodType { get; set; }
    public RhFactor RhFactor { get; set; }
    public int Quantity { get; set; }

    public Domain.Entities.DonationReceiver ToEntity(Domain.Entities.Receiver receiver, BloodStock bloodStock)
        => new(receiver.Id, bloodStock.Id, DonationDate, Quantity);
}
