using BloodDonationDb.Domain.ValueObjects;
using BloodDonationDb.Exceptions;
using FluentValidation;

namespace BloodDonationDb.Application.Commands.Donor.Register;
public class RegisterDonorValidator : AbstractValidator<RegisterDonorCommand>
{
    public RegisterDonorValidator()
    {
        RuleFor(donor => donor.Name).NotEmpty().WithMessage(ResourceMessageException.NAME_EMPTY);
        RuleFor(donor => donor.Email).NotEmpty().WithMessage(ResourceMessageException.EMAIL_EMPTY);
        RuleFor(donor => donor.BirthDate).NotEmpty().WithMessage(ResourceMessageException.BIRTH_DATE_EMPTY);
        RuleFor(donor => donor.Gender).NotEmpty().IsInEnum().WithMessage(ResourceMessageException.GENDER_NOT_SUPPORTED);
        RuleFor(donor => donor.Weight).NotEmpty().InclusiveBetween(50, 140).WithMessage(ResourceMessageException.WEIGHT_NOT_ALLOWED);
        RuleFor(donor => donor.BloodType).NotEmpty().IsInEnum().WithMessage(ResourceMessageException.BLOOD_TYPE_NOT_SUPPOTED);
        RuleFor(donor => donor.RhFactor).NotEmpty().IsInEnum().WithMessage(ResourceMessageException.RH_FACTOR_NOT_SUPPORTED);
        RuleFor(donor => donor.Address).ChildRules(addressRule =>
        {
            addressRule.RuleFor(address => address!.Street).NotEmpty().WithMessage(ResourceMessageException.ADDRESS_STREET_EMPTY);
            addressRule.RuleFor(address => address!.Number).NotEmpty().WithMessage(ResourceMessageException.ADDRESS_NUMBER_EMPTY);
            addressRule.RuleFor(address => address!.City).NotEmpty().WithMessage(ResourceMessageException.ADDRESS_CITY_EMPTY);
            addressRule.RuleFor(address => address!.State).NotEmpty().WithMessage(ResourceMessageException.ADDRESS_STATE_EMPTY);
            addressRule.RuleFor(address => address!.ZipCode).NotEmpty().WithMessage(ResourceMessageException.ADDRESS_ZIP_EMPTY);
            addressRule.RuleFor(address => address!.Country).NotEmpty().WithMessage(ResourceMessageException.ADDRESS_COUNTRY_EMPTY);
        });
        When(donor => !string.IsNullOrEmpty(donor.Email), () =>
        {
            RuleFor(donor => donor.Email).EmailAddress().WithMessage(ResourceMessageException.EMAIL_INVALID);
        });
        When(donor => !donor.BirthDate.Equals(DateTime.MinValue), () =>
        {
            RuleFor(donor => donor.BirthDate.Year).GreaterThan(DateTime.UtcNow.Year - BloodDonationRuleConstants.AGE_MAX_DONATION)
            .WithMessage(ResourceMessageException.BIRTH_DATE_NOT_ALLOWED);
            RuleFor(donor => donor.BirthDate).LessThan(DateTime.UtcNow)
            .WithMessage(ResourceMessageException.BIRTH_DATE_INVALID);
        });
    }


}
