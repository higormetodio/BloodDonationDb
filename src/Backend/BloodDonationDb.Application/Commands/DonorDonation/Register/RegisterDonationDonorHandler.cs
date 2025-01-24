using BloodDonationDb.Application.Models.DonorDonation;
using BloodDonationDb.Domain.Repositories.BloodStock;
using BloodDonationDb.Domain.Repositories.DonationDonor;
using BloodDonationDb.Domain.Repositories.Donor;
using BloodDonationDb.Domain.SeedWorks;
using MediatR;

namespace BloodDonationDb.Application.Commands.DonorDonation.Register;
public class RegisterDonationDonorHandler : IRequestHandler<RegisterDonationDonorCommand, RegisterDonationDonorViewModel>
{
    private readonly IDonationDonorWriteOnlyRepository _donationDonorWriteOnlyRepository;
    private readonly IDonorReadOnlyRepository _donorReadOnlyRepository;
    private readonly IBloodStockReadOnlyRepository _bloodStockReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterDonationDonorHandler(IDonationDonorWriteOnlyRepository donationDonorWriteOnlyRepository, 
        IDonorReadOnlyRepository donorReadOnlyRepository,
        IBloodStockReadOnlyRepository bloodStockReadOnlyRepository,
        IUnitOfWork unitOfWork)
    {
        _donationDonorWriteOnlyRepository = donationDonorWriteOnlyRepository;
        _donorReadOnlyRepository = donorReadOnlyRepository;
        _bloodStockReadOnlyRepository = bloodStockReadOnlyRepository;
        _unitOfWork = unitOfWork;
        
    }

    public async Task<RegisterDonationDonorViewModel> Handle(RegisterDonationDonorCommand request, CancellationToken cancellationToken)
    {
        var donor = await _donorReadOnlyRepository.GetDonorByEmailAsync(request.Email!);

        var bloodStock = await _bloodStockReadOnlyRepository.GetBloodStockAsync(donor.BloodType, donor.RhFactor);

        var donationDonor = request.ToEntity(donor, bloodStock);

        await _donationDonorWriteOnlyRepository.AddDonationDonorAsync(donationDonor);

        await _unitOfWork.CommitAsync();

        var registerDonationDonorViewModel = RegisterDonationDonorViewModel.FromEntity(donor, bloodStock, donationDonor);

        return registerDonationDonorViewModel;
    }
}
