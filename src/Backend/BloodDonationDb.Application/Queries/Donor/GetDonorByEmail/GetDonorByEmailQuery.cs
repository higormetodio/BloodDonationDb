using BloodDonationDb.Application.Models;
using BloodDonationDb.Application.Models.Donor;
using MediatR;

namespace BloodDonationDb.Application.Queries.Donor.GetDonor;
public class GetDonorByEmailQuery : IRequest<ResultViewModel<DonorViewModel>>
{
    public GetDonorByEmailQuery(string email)
    {
        Email = email;
    }

    public string Email { get; private set; }
}
