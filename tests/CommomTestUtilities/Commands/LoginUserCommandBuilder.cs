using BloodDonationDb.Application.Commands.Login;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommomTestUtilities.Commands;
public class LoginUserCommandBuilder
{
    public static LoginUserCommand Builder()
    {
        return new Faker<LoginUserCommand>()
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.Password, f => f.Internet.Password());
    }
}
