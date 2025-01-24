using BloodDonationDb.Application.Models;
using BloodDonationDb.Application.Models.DonorDonation;
using MediatR;
using BloodDonationDb.Domain.Entities;

namespace BloodDonationDb.Application.Commands.DonorDonation.Register;
public class RegisterDonationDonorCommand : IRequest<ResultViewModel<RegisterDonationDonorViewModel>>
{
    public string? Email { get; set; }
    public DateTime DonationDate { get; set; }
    public int Quantity { get; set; }

    public DonationDonor ToEntity(Domain.Entities.Donor donor, BloodStock bloodStock)
        => new(donor.Id, bloodStock.Id, DonationDate, Quantity);
}
