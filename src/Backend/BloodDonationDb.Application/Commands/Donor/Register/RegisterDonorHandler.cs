using BloodDonationDb.Application.Models.Donor;
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

    public RegisterDonorHandler(IDonorWriteOnlyRepository donorWriteOnlyRepository, IDonorReadOnlyRepository donorReadOnlyRepository, IUnitOfWork unitOfWork)
    {
        _donorWriteOnlyRepository = donorWriteOnlyRepository;
        _donorReadOnlyRepository = donorReadOnlyRepository;
        _unitOfWork = unitOfWork;        
    }

    public async Task<RegisterDonorViewModel> Handle(RegisterDonorCommand request, CancellationToken cancellationToken)
    {
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
