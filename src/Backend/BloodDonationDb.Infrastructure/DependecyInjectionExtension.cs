using BloodDonationDb.Comunication.Mediator;
using BloodDonationDb.Domain.Repositories.Token;
using BloodDonationDb.Domain.Repositories.User;
using BloodDonationDb.Domain.Security.Criptography;
using BloodDonationDb.Domain.Security.Tokens;
using BloodDonationDb.Domain.SeedWorks;
using BloodDonationDb.Infrastructure.Extensions;
using BloodDonationDb.Infrastructure.Persistence;
using BloodDonationDb.Infrastructure.Persistence.MongoDb;
using BloodDonationDb.Infrastructure.Persistence.Repositories;
using BloodDonationDb.Infrastructure.Security.Criptography;
using BloodDonationDb.Infrastructure.Security.Tokens.Access.Generator;
using BloodDonationDb.Infrastructure.Security.Tokens.Refresh;
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

        if (configuration.IsUnitTestEnviroment())
        {
            return;
        }

        AddDbContextSqlServer(services, configuration);

        //AddMongoDb(services, configuration);
        //AddInterceptors(services);
        //AddBackGroundJobs(services);

    }

    private static void AddDbContextSqlServer(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.ConnectionStringSql();

        services.AddDbContext<BloodDonationDbContext>(options =>
            options.UseSqlServer(connectionString));
    }

    public static void AddRepositories(IServiceCollection service)
    {
        service.AddScoped<IUserWriteOnlyRepository, UserRepository>();
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
    }

    //private static void AddMongoDb(IServiceCollection services, IConfiguration configuration)
    //{
    //    services.AddSingleton(sp =>
    //    {
    //        var options = new MongoDbOptions();

    //        configuration.GetSection("MongoDb").Bind(options);

    //        return options;
    //    });

    //    services.AddSingleton<IMongoClient>(sp =>
    //    {
    //        var options = sp.GetService<MongoDbOptions>();

    //        return new MongoClient(options!.ConnectionString);
    //    });

    //    services.AddTransient(sp =>
    //    {
    //        var options = sp.GetService<MongoDbOptions>();
    //        var client = sp.GetService<IMongoClient>();

    //        return client!.GetDatabase(options!.Database);
    //    });
    //}

    //private static void AddInterceptors(IServiceCollection services)
    //{
    //    services.AddSingleton<PublishDomainEventsToOutBoxMessagesInterceptor>();
    //}

    //private static void AddBackGroundJobs(IServiceCollection services)
    //{
    //    services.AddHostedService<ProcessOutboxMessageJob>();
    //}
}