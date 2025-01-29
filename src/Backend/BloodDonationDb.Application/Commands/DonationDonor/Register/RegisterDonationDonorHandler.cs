using BloodDonationDb.Application.Models.DonorDonation;
using BloodDonationDb.Domain.Enums;
using BloodDonationDb.Domain.Repositories.BloodStock;
using BloodDonationDb.Domain.Repositories.DonationDonor;
using BloodDonationDb.Domain.Repositories.Donor;
using BloodDonationDb.Domain.SeedWorks;
using BloodDonationDb.Exceptions;
using BloodDonationDb.Exceptions.ExceptionsBase;
using MediatR;

namespace BloodDonationDb.Application.Commands.DonationDonor.Register;
public class RegisterDonationDonorHandler : IRequestHandler<RegisterDonationDonorCommand, RegisterDonationDonorViewModel>
{
    private readonly IDonationDonorWriteOnlyRepository _donationDonorWriteOnlyRepository;
    private readonly IDonorReadOnlyRepository _donorReadOnlyRepository;
    private readonly IDonorUpdateOnlyRepository _donorUpdateOnlyRepository;
    private readonly IBloodStockReadOnlyRepository _bloodStockReadOnlyRepository;
    private readonly IBloodStockUpdateOnlyRepository _bloodStockUpdateOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterDonationDonorHandler(IDonationDonorWriteOnlyRepository donationDonorWriteOnlyRepository,
        IDonorReadOnlyRepository donorReadOnlyRepository,
        IDonorUpdateOnlyRepository donorUpdateOnlyRepository,
        IBloodStockReadOnlyRepository bloodStockReadOnlyRepository,
        IBloodStockUpdateOnlyRepository bloodStockUpdateOnlyRepository,
        IUnitOfWork unitOfWork)
    {
        _donationDonorWriteOnlyRepository = donationDonorWriteOnlyRepository;
        _donorReadOnlyRepository = donorReadOnlyRepository;
        _bloodStockReadOnlyRepository = bloodStockReadOnlyRepository;
        _bloodStockUpdateOnlyRepository = bloodStockUpdateOnlyRepository;
        _unitOfWork = unitOfWork;
        _donorUpdateOnlyRepository = donorUpdateOnlyRepository;
    }

    public async Task<RegisterDonationDonorViewModel> Handle(RegisterDonationDonorCommand request, CancellationToken cancellationToken)
    {
        var donor = await _donorReadOnlyRepository.GetDonorByEmailAsync(request.Email!);

        Validate(request, donor);

        var bloodStock = await _bloodStockReadOnlyRepository.GetBloodStockAsync(donor.BloodType, donor.RhFactor);

        var donationDonor = request.ToEntity(donor, bloodStock);

        await _donationDonorWriteOnlyRepository.AddDonationDonorAsync(donationDonor);

        bloodStock.UpdateStockDonationDonor(donationDonor.Quantity);

        _bloodStockUpdateOnlyRepository.UpdateBloodStock(bloodStock);

        donor.UpdateLastDonation(donationDonor.When);

        _donorUpdateOnlyRepository.UpdateDonor(donor);

        await _unitOfWork.CommitAsync();

        var registerDonationDonorViewModel = RegisterDonationDonorViewModel.FromEntity(donor, bloodStock, donationDonor);

        return registerDonationDonorViewModel;
    }

    public void Validate(RegisterDonationDonorCommand command, Domain.Entities.Donor donor)
    {
        if (donor is null)
        {
            throw new NotFoundException(ResourceMessageException.DONOR_NOT_FOUND);
        }

        var result = new RegisterDonationDonorValidator().Validate(command);

        if (!donor.IsDonor)
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure("isDonor", ResourceMessageException.DONOR_CANNOT_DONATION));
        }

        if (donor.NextDonation > command.DonationDate)
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure("nextDonation", 
                $"{ResourceMessageException.DONATION_NOT_ALLOWED_NEXT_DONATION_DATE} {donor.NextDonation.Value.Date.ToShortDateString()}"));
        }

        if (!result.IsValid)
        {
            var errorMessage = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessage);
        }


    }
}
