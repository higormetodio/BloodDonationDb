using BloodDonationDb.Application.Commands.User.Register;
using BloodDonationDb.Comunication.Mediator;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BloodDonationDb.Application;
public static class DependecyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddMediator(services);
    }

    private static void AddMediator(IServiceCollection services)
    {
      
        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<RegisterUserCommand>());
        //services.AddScoped<IMediatorHandler, MediatorHandler>();
        //services.AddScoped<IRequestHandler, RequestHandler>
    }
}
