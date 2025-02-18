using BloodDonationDb.Application.Models.DonationReceiver;
using BloodDonationDb.Domain.Entities;
using BloodDonationDb.Domain.Repositories.BloodStock;
using BloodDonationDb.Domain.Repositories.DonationReceiver;
using BloodDonationDb.Domain.Repositories.Receiver;
using BloodDonationDb.Domain.SeedWorks;
using BloodDonationDb.Exceptions.ExceptionsBase;
using BloodDonationDb.Exceptions;
using MediatR;
using BloodDonationDb.Domain.Enums;
using BloodDonationDb.Domain.Events;

namespace BloodDonationDb.Application.Commands.DonationReceiver.Register;
public class RegisterDonationReceiverHandler : IRequestHandler<RegisterDonationReceiverCommand, RegisterDonationReceiverViewModel>
{
    private readonly IDonationReceiverWriteOnlyRepository _donationReceiverWriteOnlyRepository;
    private readonly IReceiverReadOnlyRepository _receiverReadonlyRepository;
    private readonly IBloodStockReadOnlyRepository _bloodStockReadOnlyRepository;
    private readonly IBloodStockUpdateOnlyRepository _bloodStockUpdateOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterDonationReceiverHandler(
        IDonationReceiverWriteOnlyRepository donationReceiverWriteOnlyRepository, 
        IReceiverReadOnlyRepository receiverReadonlyRepository, 
        IBloodStockReadOnlyRepository bloodStockReadOnlyRepository, 
        IBloodStockUpdateOnlyRepository bloodStockUpdateOnlyRepository, 
        IUnitOfWork unitOfWork)
    {
        _donationReceiverWriteOnlyRepository = donationReceiverWriteOnlyRepository;
        _receiverReadonlyRepository = receiverReadonlyRepository;
        _bloodStockReadOnlyRepository = bloodStockReadOnlyRepository;
        _bloodStockUpdateOnlyRepository = bloodStockUpdateOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<RegisterDonationReceiverViewModel> Handle(RegisterDonationReceiverCommand request, CancellationToken cancellationToken)
    {
        var receiver = await _receiverReadonlyRepository.GetReceiverByEmail(request.Email!);

        if (receiver is null)
        {
            throw new NotFoundException(ResourceMessageException.RECEIVER_NOT_FOUND);
        }

        var bloodStock = await _bloodStockReadOnlyRepository.GetBloodStockAsync(request.BloodType, request.RhFactor);

        var donationReceiver = request.ToEntity(receiver, bloodStock);

        await _donationReceiverWriteOnlyRepository.AddDonationReceiverAsync(donationReceiver);

        bloodStock.UpdateStockDonationReceiver(donationReceiver.Quantity);

        _bloodStockUpdateOnlyRepository.UpdateBloodStock(bloodStock);

        if (bloodStock.MinimumQuantityReached)
        {
            donationReceiver.AddDomainEvent(
                new BloodStockMinimumQuantityDomainEvent(
                    bloodStock.Id, bloodStock.BloodType, bloodStock.RhFactor, bloodStock.Quantity)
                );
        }

        await _unitOfWork.CommitAsync();

        var registerDonationReceiverViewModel = RegisterDonationReceiverViewModel.FromEntity(receiver, bloodStock, donationReceiver);

        return registerDonationReceiverViewModel;
    }
}
