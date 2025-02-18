using BloodDonationDb.Application.Models.Receiver;
using BloodDonationDb.Domain.Enums;
using BloodDonationDb.Domain.ValueObjects;
using MediatR;

namespace BloodDonationDb.Application.Commands.Receiver;
public class RegisterReceiverCommand : IRequest<RegisterReceiverViewModel>
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public BloodType BloodType { get; set; }
    public RhFactor RhFactor { get; set; }
    public Address? Address { get; set; }

    public Domain.Entities.Receiver ToEntity()
     => new Domain.Entities.Receiver(Name!, Email!, BloodType, RhFactor, Address!);
}
