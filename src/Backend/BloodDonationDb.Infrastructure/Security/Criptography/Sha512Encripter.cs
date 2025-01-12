using BloodDonationDb.Domain.Security.Criptography;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Security.Cryptography;
using System.Text;

namespace BloodDonationDb.Infrastructure.Security.Criptography;
public class Sha512Encripter : IPasswordEncripter
{
    private readonly string _additionalKey;

    public Sha512Encripter(string additionalKey)
    {
        _additionalKey = additionalKey;
    }

    public string Encript(string password)
    {
        var newPassword = $"{password}{_additionalKey}";

        var bytes = Encoding.UTF8.GetBytes(newPassword);
        var hashBytes = SHA512.HashData(bytes);

        return StringBytes(hashBytes);
    }     

    public bool IsValid(string password, string passwordHash)
    {
        var hashedPassword = Encript(password);

        return hashedPassword.Equals(passwordHash, StringComparison.OrdinalIgnoreCase);
    }

    private string StringBytes(byte[] hashBytes)
    {
        var sb = new StringBuilder();

        foreach (var b in hashBytes)
        {
            var hex = b.ToString("X2");
            sb.Append(hex);
        }

        return sb.ToString();
    }
}
