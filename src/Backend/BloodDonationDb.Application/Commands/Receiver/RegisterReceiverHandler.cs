using BloodDonationDb.Application.Models.Receiver;
using BloodDonationDb.Domain.Repositories.Receiver;
using BloodDonationDb.Domain.SeedWorks;
using MediatR;

namespace BloodDonationDb.Application.Commands.Receiver;
public class RegisterReceiverHandler : IRequestHandler<RegisterReceiverCommand, RegisterReceiverViewModel>
{
    public readonly IReceiverWriteOnlyRepository _receiverWriteOnlyRepository;
    public readonly IUnitOfWork _unitOfWork;

    public RegisterReceiverHandler(IReceiverWriteOnlyRepository receiverWriteOnlyRepository, IUnitOfWork unitOfWork)
    {
        _receiverWriteOnlyRepository = receiverWriteOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<RegisterReceiverViewModel> Handle(RegisterReceiverCommand request, CancellationToken cancellationToken)
    {
        var receiver = request.ToEntity();

        await _receiverWriteOnlyRepository.AddReceiverAsync(receiver);

        await _unitOfWork.CommitAsync();

        var registerReceiverViewModel = RegisterReceiverViewModel.FromEntity(receiver);

        return registerReceiverViewModel;
    }
}
