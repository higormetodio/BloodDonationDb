using BloodDonationDb.Application.Commands.User.Register;
using BloodDonationDb.Application.Services.ConsultaCep;
using BloodDonationDb.Application.Services.GetCep;
using BloodDonationDb.Application.Services.ReportPdf;
using BloodDonationDb.Domain.Entities;
using BloodDonationDb.Infrastructure.Persistence.MongoDb;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace BloodDonationDb.Application;
public static class DependecyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddMediator(services);
        AssApplicationServices(services);
        AddMongoDb(services, configuration);
    }

    private static void AddMediator(IServiceCollection services)
    {

        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<RegisterUserCommand>());
    }

    private static void AssApplicationServices(IServiceCollection services)
    {
        services.AddScoped<IGetCepService, GetCepService>();
        services.AddScoped<IReportPdfService<BloodStock>, ReportPdfService<BloodStock>>();
        services.AddScoped<IReportPdfService<DonationDonor>, ReportPdfService<DonationDonor>>();
    }

    private static void AddMongoDb(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(sp =>
        {
            var options = new MongoDbOptions();

            configuration.GetSection("MongoDb").Bind(options);

            return options;
        });

        services.AddSingleton<IMongoClient>(sp =>
        {
            var options = sp.GetService<MongoDbOptions>();

            return new MongoClient(options!.ConnectionString);
        });

        services.AddSingleton(sp =>
        {
            var options = sp.GetService<MongoDbOptions>();
            var client = sp.GetRequiredService<IMongoClient>();
            var database = client!.GetDatabase(options!.Database);

            return database.GetCollection<BloodStock>("BloodStock");
        });
    }
}
