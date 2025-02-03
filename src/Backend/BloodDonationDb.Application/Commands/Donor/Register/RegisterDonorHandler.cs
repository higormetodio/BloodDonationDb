using BloodDonationDb.Application.Models.Donor;
using BloodDonationDb.Application.Services.ConsultaCep;
using BloodDonationDb.Domain.Repositories.Donor;
using BloodDonationDb.Domain.SeedWorks;
using BloodDonationDb.Exceptions;
using BloodDonationDb.Exceptions.ExceptionsBase;
using MediatR;

namespace BloodDonationDb.Application.Commands.Donor.Register;
public class RegisterDonorHandler : IRequestHandler<RegisterDonorCommand, RegisterDonorViewModel>
{
    private readonly IDonorWriteOnlyRepository _donorWriteOnlyRepository;
    private readonly IDonorReadOnlyRepository _donorReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGetCepService _getCepService;

    public RegisterDonorHandler(IDonorWriteOnlyRepository donorWriteOnlyRepository, IDonorReadOnlyRepository donorReadOnlyRepository, IUnitOfWork unitOfWork, IGetCepService getCepService)
    {
        _donorWriteOnlyRepository = donorWriteOnlyRepository;
        _donorReadOnlyRepository = donorReadOnlyRepository;
        _unitOfWork = unitOfWork;
        _getCepService = getCepService;
    }

    public async Task<RegisterDonorViewModel> Handle(RegisterDonorCommand request, CancellationToken cancellationToken)
    {

        var address = await _getCepService.GetCepAsync(request.Address!.ZipCode);
        
        if (address is null)
        {
            throw new NotFoundException(ResourceMessageException.CEP_NOT_FOUND);
        }

        request.Address = address.ToEntity(request.Address.Number, request.Address.Country);

        await Validate(request);

        var donor = request.ToEntity();

        await _donorWriteOnlyRepository.AddDonorAsync(donor);

        await _unitOfWork.CommitAsync();

        var responseRegisterDonor = RegisterDonorViewModel.FromEntity(donor);

        return responseRegisterDonor;
    }

    private async Task Validate(RegisterDonorCommand command)
    {
        var validator = new RegisterDonorValidator();

        var result = await validator.ValidateAsync(command);

        var donorExist = await _donorReadOnlyRepository.ExistActiveDonorWithEmail(command.Email!);

        if (donorExist)
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure("email", ResourceMessageException.EMAIL_ALREADY_REGISTER));
        }

        if (!result.IsValid)
        {
            var errorMessage = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessage);
        }
    }
}
