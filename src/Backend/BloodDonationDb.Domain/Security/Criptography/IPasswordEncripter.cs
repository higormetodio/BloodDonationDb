namespace BloodDonationDb.Domain.Security.Criptography;
public interface IPasswordEncripter
{
    public string Encript(string password);

    public bool IsValid(string password, string passwordHash);
}
