using BloodDonationDb.Application.Queries.Donor.GetDonor;
using BloodDonationDb.Application.Queries.Donor.GetDonorDonationsByEmail;
using BloodDonationDb.Domain.Entities;
using Bogus;
using CommomTestUtilities.Domain.Entities;

namespace CommomTestUtilities.Queries;
public class GetDonorDonationsByEmailQueryBuilder
{
    public static (Donor donor, GetDonorDonationsByEmailQuery) Builder()
    {
        var donor = DonorBuilder.Builder();

        var faker = new Faker<GetDonorDonationsByEmailQuery>()
            .RuleFor(d => d.Email, f => donor.Email);

        return (donor, faker);
    }
}
