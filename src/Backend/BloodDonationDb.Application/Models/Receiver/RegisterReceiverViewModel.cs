namespace BloodDonationDb.Application.Models.Receiver;
public class RegisterReceiverViewModel
{
    public RegisterReceiverViewModel(Guid receiverId, string name)
    {
        ReceiverId = receiverId;
        Name = name;
    }

    public Guid ReceiverId { get; private set; }
    public string Name { get; private set; }

    public static RegisterReceiverViewModel FromEntity(Domain.Entities.Receiver receiver)
        => new RegisterReceiverViewModel(receiver.Id, receiver.Name);
}
