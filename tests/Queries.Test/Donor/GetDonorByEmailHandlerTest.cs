using BloodDonationDb.Application.Queries.Donor.GetDonorByEmail;
using BloodDonationDb.Domain.Enums;
using BloodDonationDb.Exceptions;
using BloodDonationDb.Exceptions.ExceptionsBase;
using CommomTestUtilities.Queries;
using CommomTestUtilities.Repositories.Donor;
using FluentAssertions;

namespace Queries.Test.Donor;
public class GetDonorByEmailTest
{
    [Fact]
    public async Task Success()
    {
        var (donor, command) = GetDonorByEmailQueryBuilder.Builder();

        var handler = CreateHandler(donor);

        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.DonorId.Should().NotBe(default(Guid));
        result.Name.Should().NotBeNullOrEmpty();
        result.Email.Should().NotBeNullOrEmpty();
        result.BirthDate.Should().NotBe(default(DateTime));
        result.Gender.Should().BeOneOf(Gender.Male, Gender.Female);
        result.Weight.Should().BeInRange(140, 210);
        result.BloodType.Should().BeOneOf(["A", "B", "O", "AB"]);
        result.RhFactor.Should().BeOneOf(["Positive", "Negative"]);
        result.Address.Should().NotBeNull();
        result.Active.Should().BeTrue();
        result.LastDonation.Should().BeNull();
        result.NextDonation.Should().BeNull();
    }

    [Fact]
    public async Task Error_Donor_Not_Found()
    {
        var (_, command) = GetDonorByEmailQueryBuilder.Builder();

        var handler = CreateHandler();

        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>()
            .Where(e => e.GetErrorMessages().Count == 1 && e.GetErrorMessages().Contains(ResourceMessageException.DONOR_NOT_FOUND));
    }

    public GetDonorByEmailHandler CreateHandler(BloodDonationDb.Domain.Entities.Donor? donor = null)
    {
        var donoReadOnlyRepository = new DonorReadOnlyRepositoryBuilder();

        if (donor is not null)
        {
            donoReadOnlyRepository.GetDonorByEmail(donor);
        }

        return new GetDonorByEmailHandler(donoReadOnlyRepository.Builder());
    }
}
