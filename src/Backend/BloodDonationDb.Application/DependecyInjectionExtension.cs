using BloodDonationDb.Application.Commands.User.Register;
using BloodDonationDb.Application.Services.ConsultaCep;
using BloodDonationDb.Application.Services.GetCep;
using BloodDonationDb.Comunication.Mediator;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BloodDonationDb.Application;
public static class DependecyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddMediator(services);
        AssApplicationServices(services);
    }

    private static void AddMediator(IServiceCollection services)
    {
      
        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<RegisterUserCommand>());
    }

    private static void AssApplicationServices(IServiceCollection services)
    {
        services.AddScoped<IGetCepService, GetCepService>();
    }
}
