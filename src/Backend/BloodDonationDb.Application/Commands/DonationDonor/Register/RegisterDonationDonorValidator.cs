using BloodDonationDb.Exceptions;
using FluentValidation;

namespace BloodDonationDb.Application.Commands.DonationDonor.Register;
public class RegisterDonationDonorValidator : AbstractValidator<RegisterDonationDonorCommand>
{
    public RegisterDonationDonorValidator()
    {
        RuleFor(donation => donation.DonationDate).NotEmpty().WithMessage(ResourceMessageException.DONATION_DATE_EMPTY);
        RuleFor(donation => donation.Quantity).NotEmpty().InclusiveBetween(420, 470).WithMessage(ResourceMessageException.DONATION_QUANTITY_NOT_ALLOWED);
        When(donation => !donation.DonationDate.Equals(DateTime.MinValue), () =>
        {
            RuleFor(donation => donation.DonationDate).LessThanOrEqualTo(DateTime.UtcNow).WithMessage(ResourceMessageException.DONATION_DATE_GREATER_CURRENT_DATE);
        });
        
    }
}
