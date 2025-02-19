using BloodDonationDb.Application.Models.Donor;
using MediatR;

namespace BloodDonationDb.Application.Queries.Donor.GetDonorDonationsByEmail;
public class GetDonorDonationsByEmailQuery : IRequest<DonorDonationsViewModel>
{
    public GetDonorDonationsByEmailQuery(string email)
    {
        Email = email;
    }

    public GetDonorDonationsByEmailQuery()
    {       
    }

    public string Email { get; private set; }
}

