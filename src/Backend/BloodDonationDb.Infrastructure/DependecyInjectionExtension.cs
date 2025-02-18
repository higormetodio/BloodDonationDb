using BloodDonationDb.Comunication.Mediator;
using BloodDonationDb.Domain.Entities;
using BloodDonationDb.Domain.Events;
using BloodDonationDb.Domain.Repositories.BloodStock;
using BloodDonationDb.Domain.Repositories.DonationDonor;
using BloodDonationDb.Domain.Repositories.DonationReceiver;
using BloodDonationDb.Domain.Repositories.Donor;
using BloodDonationDb.Domain.Repositories.Receiver;
using BloodDonationDb.Domain.Repositories.Token;
using BloodDonationDb.Domain.Repositories.User;
using BloodDonationDb.Domain.Security.Criptography;
using BloodDonationDb.Domain.Security.Tokens;
using BloodDonationDb.Domain.SeedWorks;
using BloodDonationDb.Domain.Services.LoggedUser;
using BloodDonationDb.Infrastructure.BackgroundServices;
using BloodDonationDb.Infrastructure.Extensions;
using BloodDonationDb.Infrastructure.Interceptors;
using BloodDonationDb.Infrastructure.Persistence;
using BloodDonationDb.Infrastructure.Persistence.MongoDb;
using BloodDonationDb.Infrastructure.Persistence.Repositories;
using BloodDonationDb.Infrastructure.Security.Criptography;
using BloodDonationDb.Infrastructure.Security.Tokens.Access.Generator;
using BloodDonationDb.Infrastructure.Security.Tokens.Access.Validator;
using BloodDonationDb.Infrastructure.Security.Tokens.Refresh;
using BloodDonationDb.Infrastructure.Services.LoggedUser;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace BloodDonationDb.Infrastructure;

public static class DependecyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        AddRepositories(services);
        AddPasswordEncripter(services, configuration);
        AddAccessToken(services, configuration);
        AddLoggedUser(services);


        if (configuration.IsUnitTestEnviroment())
        {
            return;
        }

        AddDbContextSqlServer(services, configuration);

        AddMediatorHandler(services);
        AddInterceptor(services);
        AddBackGroundJobs(services);

    }

    private static void AddDbContextSqlServer(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.ConnectionStringSql();

        services.AddDbContext<BloodDonationDbContext>(
            (sp, options) =>
            {
                var interceptor = sp.GetService<ConvertDomainEventsToOutBoxMessagesInterceptor>();

                options.UseSqlServer(connectionString).AddInterceptors(interceptor!);
            });
    }

    public static void AddRepositories(IServiceCollection service)
    {
        service.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        service.AddScoped<IUserReadOnlyRepository, UserRepository>();
        service.AddScoped<IDonorWriteOnlyRepository, DonorRepository>();
        service.AddScoped<IDonorReadOnlyRepository, DonorRepository>();
        service.AddScoped<IDonorUpdateOnlyRepository, DonorRepository>();
        service.AddScoped<IBloodStockReadOnlyRepository, BloodStockRepository>();
        service.AddScoped<IBloodStockUpdateOnlyRepository, BloodStockRepository>();
        service.AddScoped<IDonationDonorWriteOnlyRepository, DonationDonorRepository>();
        service.AddScoped<IReceiverWriteOnlyRepository, ReceiverRepository>();
        service.AddScoped<IDonationReceiverWriteOnlyRepository, DonationReceiverRepository>();
        service.AddScoped<IReceiverReadOnlyRepository, ReceiverRepository>();
        service.AddScoped<IUnitOfWork, UnitOfWork>();
        service.AddScoped<ITokenRepository, TokenRepository>();
    }

    public static void AddPasswordEncripter(IServiceCollection service, IConfiguration configuration)
    {
        var additionalKey = configuration.GetValue<string>("Settings:Passwords:AdditionalKey");

        service.AddScoped<IPasswordEncripter>(options => new Sha512Encripter(additionalKey!));
    }

    public static void AddAccessToken(IServiceCollection service, IConfiguration configuration)
    {
        var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpirationTimeMinutes");
        var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

        service.AddScoped<IAccessTokenGenerator>(options => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
        service.AddScoped<IRefreshTokenGenerator>(options => new RefreshTokenGenerator());
        service.AddScoped<IAccessTokenValidator>(options => new JwtTokenValidator(signingKey!));
    }

    public static void AddLoggedUser(IServiceCollection service) => service.AddScoped<ILoggedUser, LoggedUser>();

    private static void AddMediatorHandler(IServiceCollection services)
    {
        services.AddScoped<IMediatorHandler, MediatorHandler>();
    }

    private static void AddInterceptor(IServiceCollection services)
    {
        services.AddSingleton<ConvertDomainEventsToOutBoxMessagesInterceptor>();
    }

    private static void AddBackGroundJobs(IServiceCollection services)
    {
        services.AddHostedService<ProcessOutboxMessageJob>();
    }
}