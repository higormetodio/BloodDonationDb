using BloodDonationDb.Application.Queries.Donor.GetDonorDonationsByEmail;
using BloodDonationDb.Exceptions;
using BloodDonationDb.Exceptions.ExceptionsBase;
using CommomTestUtilities.Queries;
using CommomTestUtilities.Repositories.Donor;
using FluentAssertions;

namespace Queries.Test.Donor;
public class GetDonorDonationsByEmailHandlerTest
{
    [Fact]
    public async Task Success()
    {
        var (donor, command) = GetDonorDonationsByEmailQueryBuilder.Builder();

        var handler = CreateHandler(donor);

        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.DonorId.Should().NotBe(default(Guid));
        result.Name.Should().NotBeNullOrEmpty();
        result.Email.Should().NotBeNullOrEmpty();
        result.BloodType.Should().BeOneOf(["A", "B", "O", "AB"]);
        result.RhFactor.Should().BeOneOf(["Positive", "Negative"]);
    }

    [Fact]
    public async Task Error_Donor_Not_Found()
    {
        var (_, command) = GetDonorDonationsByEmailQueryBuilder.Builder();

        var handler = CreateHandler();

        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>()
            .Where(e => e.GetErrorMessages().Count == 1 && e.GetErrorMessages().Contains(ResourceMessageException.DONOR_NOT_FOUND));
    }

    public GetDonorDonationsByEmailHandler CreateHandler(BloodDonationDb.Domain.Entities.Donor? donor = null)
    {
        var donoReadOnlyRepository = new DonorReadOnlyRepositoryBuilder();

        if (donor is not null)
        {
            donoReadOnlyRepository.GetDonorDonationsByEmail(donor);
        }

        return new GetDonorDonationsByEmailHandler(donoReadOnlyRepository.Builder());
    }
}
