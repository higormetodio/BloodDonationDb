using BloodDonationDb.Application.Models.DonorDonation;
using BloodDonationDb.Domain.Entities;
using BloodDonationDb.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationDb.Application.Models.Donor;
public class DonorDonationsViewModel
{
    public DonorDonationsViewModel(Guid donorId, string? name, string? email, string bloodType, string rhFactor, bool isDonor, IList<DonationDonorViewModel> donations)
    {
        DonorId = donorId;
        Name = name;
        Email = email;
        BloodType = bloodType;
        RhFactor = rhFactor;
        IsDonor = isDonor;
        Donations = donations;
    }

    public Guid DonorId { get; private set; }
    public string? Name { get; private set; }
    public string? Email { get; private set; }
    public string BloodType { get; private set; }
    public string RhFactor { get; private set; }
    public bool IsDonor { get; private set; }
    public IList<DonationDonorViewModel> Donations { get; private set; }
}
