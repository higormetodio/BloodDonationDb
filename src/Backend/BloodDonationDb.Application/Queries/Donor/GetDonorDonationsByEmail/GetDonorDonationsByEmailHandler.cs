using BloodDonationDb.Application.Models.Donor;
using BloodDonationDb.Application.Models.DonorDonation;
using BloodDonationDb.Domain.Repositories.Donor;
using BloodDonationDb.Exceptions;
using BloodDonationDb.Exceptions.ExceptionsBase;
using MediatR;

namespace BloodDonationDb.Application.Queries.Donor.GetDonorDonationsByEmail;
public class GetDonorDonationsByEmailHandler : IRequestHandler<GetDonorDonationsByEmailQuery, DonorDonationsViewModel>
{
    private readonly IDonorReadOnlyRepository _donorReadOnlyRepository;

    public GetDonorDonationsByEmailHandler(IDonorReadOnlyRepository donorReadOnlyRepository)
    {
        _donorReadOnlyRepository = donorReadOnlyRepository;
    }

    public async Task<DonorDonationsViewModel> Handle(GetDonorDonationsByEmailQuery request, CancellationToken cancellationToken)
    {
        var donor = await _donorReadOnlyRepository.GetDonorDonationsByEmailAsync(request.Email);

        if (donor is null)
        {
            throw new NotFoundException(ResourceMessageException.DONOR_NOT_FOUND);
        }

        var donationDonorViewModel = donor.Donations.Select(DonationDonorViewModel.FromEntity);

        var donorDonationsViewModel = new DonorDonationsViewModel(
            donor.Id, donor.Name, donor.Email, donor.BloodType.ToString(), donor.RhFactor.ToString(), donor.IsDonor, donationDonorViewModel.ToList());

        return donorDonationsViewModel;
    }
}
