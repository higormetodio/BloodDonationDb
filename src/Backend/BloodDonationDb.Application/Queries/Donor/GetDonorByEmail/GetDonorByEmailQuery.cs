using BloodDonationDb.Application.Models.Donor;
using MediatR;

namespace BloodDonationDb.Application.Queries.Donor.GetDonor;
public class GetDonorByEmailQuery : IRequest<DonorViewModel>
{
    public GetDonorByEmailQuery(string email)
    {
        Email = email;
    }

    public GetDonorByEmailQuery()
    {}

    public string Email { get; private set; }
}
