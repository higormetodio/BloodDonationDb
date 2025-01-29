using BloodDonationDb.Application.Queries.Donor.GetDonor;
using BloodDonationDb.Domain.Entities;
using Bogus;
using CommomTestUtilities.Domain.Entities;

namespace CommomTestUtilities.Queries;
public class GetDonorByEmailQueryBuilder
{
    public static (Donor donor, GetDonorByEmailQuery) Builder()
    {
        var donor = DonorBuilder.Builder();

        var faker = new Faker<GetDonorByEmailQuery>()
            .RuleFor(d => d.Email, f => donor.Email);

        return (donor, faker);
    }
    
}
