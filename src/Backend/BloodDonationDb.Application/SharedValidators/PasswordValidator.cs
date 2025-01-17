using BloodDonationDb.Exceptions;
using FluentValidation;
using FluentValidation.Validators;
using System.Text.RegularExpressions;

namespace BloodDonationDb.Application.SharedValidators;
public class PasswordValidator<T> : PropertyValidator<T, string>
{
    public override string Name => "PasswordValidator";

    public override bool IsValid(ValidationContext<T> context, string password)
    {
        var regex = new Regex(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$").IsMatch(password);

        if (string.IsNullOrWhiteSpace(password))
        {
            context.MessageFormatter.AppendArgument("ErrorMessage", ResourceMessageException.PASSWORD_EMPTY);
            
            return false;
        }

        if (!regex)
        {
            context.MessageFormatter.AppendArgument("ErrorMessage", ResourceMessageException.PASSWORD_INVALID);

            return false;
        }

        return true;
    }

    protected override string GetDefaultMessageTemplate(string errorCode) => "{ErrorMessage}";
}
