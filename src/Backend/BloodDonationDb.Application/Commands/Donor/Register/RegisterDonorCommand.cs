using BloodDonationDb.Application.Models;
using BloodDonationDb.Application.Models.Donor;
using BloodDonationDb.Domain.Entities;
using BloodDonationDb.Domain.Enums;
using BloodDonationDb.Domain.ValueObjects;
using MediatR;

namespace BloodDonationDb.Application.Commands.Donor.Register;
public class RegisterDonorCommand : IRequest<ResultViewModel<RegisterDonorViewModel>>
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public int Weight { get; set; }
    public BloodType BloodType { get; set; }
    public RhFactor RhFactor { get; set; }
    public Address? Address { get; set; }

    public Domain.Entities.Donor ToEntity()
        => new(Name!, Email!, BirthDate, Gender, Weight, BloodType, RhFactor, Address!);
}
