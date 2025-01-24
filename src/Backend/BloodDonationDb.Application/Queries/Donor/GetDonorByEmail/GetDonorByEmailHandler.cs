﻿using BloodDonationDb.Application.Models.Donor;
using BloodDonationDb.Application.Queries.Donor.GetDonor;
using BloodDonationDb.Domain.Repositories.Donor;
using MediatR;

namespace BloodDonationDb.Application.Queries.Donor.GetDonorByEmail;
public class GetDonorByEmailHandler : IRequestHandler<GetDonorByEmailQuery, DonorViewModel>
{
    private readonly IDonorReadOnlyRepository _donorReadOnlyRepository;

    public GetDonorByEmailHandler(IDonorReadOnlyRepository donorReadOnlyRepository)
    {
        _donorReadOnlyRepository = donorReadOnlyRepository;
    }

    public async Task<DonorViewModel> Handle(GetDonorByEmailQuery request, CancellationToken cancellationToken)
    {
        var donor = await _donorReadOnlyRepository.GetDonorByEmailAsync(request.Email);

        var donorViewModel = DonorViewModel.FromEntity(donor);

        return donorViewModel;
    }
}
