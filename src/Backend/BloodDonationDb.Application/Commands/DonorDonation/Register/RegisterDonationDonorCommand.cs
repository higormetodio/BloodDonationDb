using BloodDonationDb.Application.Models.DonorDonation;
using BloodDonationDb.Domain.Entities;
using MediatR;

namespace BloodDonationDb.Application.Commands.DonorDonation.Register;
public class RegisterDonationDonorCommand : IRequest<RegisterDonationDonorViewModel>
{
    public string? Email { get; set; }
    public DateTime DonationDate { get; set; }
    public int Quantity { get; set; }

    public DonationDonor ToEntity(Domain.Entities.Donor donor, BloodStock bloodStock)
        => new(donor.Id, bloodStock.Id, DonationDate, Quantity);
}
