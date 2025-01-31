using BloodDonationDb.Domain.Enums;

namespace BloodDonationDb.Application.Models.DonorDonation;
public class DonationDonorViewModel
{
    public DonationDonorViewModel(Guid donationDonorId, int quantity, DateTime donationDate)
    {
        DonationDonorId = donationDonorId;
        Quantity = quantity;
        DonationDate = donationDate;
    }

    public Guid DonationDonorId { get; private set; }
    public int Quantity { get; private set; }
    public DateTime DonationDate { get; private set; }

    public static DonationDonorViewModel FromEntity(Domain.Entities.DonationDonor entity)
        => new(entity.Id, entity.Quantity, entity.When);
}
