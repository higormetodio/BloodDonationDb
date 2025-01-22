using BloodDonationDb.Application.Models.Token;

namespace BloodDonationDb.Application.Models.User;
public class RegisterUserViewModel
{
    public string? Name { get; set; }
    public TokenViewModel? Token { get; set; }
}
