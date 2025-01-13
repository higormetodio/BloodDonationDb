using BloodDonationDb.Domain.Security.Criptography;
using BloodDonationDb.Infrastructure.Security.Criptography;

namespace CommomTestUtilities.Criptography;
public class PasswordEncripterBuilder
{
    public static IPasswordEncripter Builder() => new Sha512Encripter("abcd1234");
}
