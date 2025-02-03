using BloodDonationDb.Application.DTOs;
using BloodDonationDb.Application.Services.ConsultaCep;
using NSubstitute;

namespace CommomTestUtilities.Services;
public class GetCepServiceBuilder
{
    public static IGetCepService Builder(EnderecoDTO dto)
    {
        var mock = Substitute.For<IGetCepService>();

        mock.GetCepAsync(Arg.Any<string>()).Returns(Task.FromResult(dto));

        return mock;
    }
}
