using Bogus;
using BloodDonationDb.Application.Commands.User.Register;

namespace CommomTestUtilities.Commands;
public class RegisterUserCommandBuilder
{
    public static RegisterUserCommand Builder()
    {
        return new Faker<RegisterUserCommand>()
            .RuleFor(user => user.Name, faker => faker.Person.FirstName)
            .RuleFor(user => user.Email, (f, user) => f.Internet.Email(user.Name))
            .RuleFor(user => user.Password, f => f.Internet.Password(memorable: true, prefix: "1Q2w3e4R#!"));
    }
}
