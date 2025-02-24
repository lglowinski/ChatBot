using System.Text.Json;
using ChatBot.Common.Auth;
using ChatBot.Common.Communication;
using ChatBot.Common.Communication.Configuration;
using ChatBot.Common.Endpoints;
using ChatBot.Common.RateLimiting;
using ChatBot.Common.TimeProvider;
using ChatBot.Users.Application;
using ChatBot.Users.Infrastructure;
using ChatBot.Users.Settings;

namespace ChatBot.Users.Composition;

public static class ServicesComposition
{
    public static IHostApplicationBuilder RegisterDependencies(this IHostApplicationBuilder builder, ILogger logger)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDefaultTimeProvider();
        builder.Services.AddEndpoints<Program>(builder.Configuration);
        builder.Services.AddJwtSettings(builder.Configuration);

        var rateLimitingSettings = new RateLimitingSettings();
        builder.Configuration.GetSection(nameof(RateLimitingSettings)).Bind(rateLimitingSettings);
        
        builder.Services.AddSingleton(rateLimitingSettings);

        builder.Services.AddRateLimiting(rateLimitingSettings);

        builder.Services.AddApplication();
        builder.AddInfrastructure("users");
        builder.Services.AddCommunication(builder.Configuration, logger);

        return builder;
    }
    
    private static IServiceCollection AddCommunication(this IServiceCollection serviceCollection, IConfiguration configuration, ILogger logger)
    {
        var useKafka = configuration.GetValue<bool>("UseKafka");
        CommunicationConfiguration communicationSettings;
        
        if (useKafka)
        {
            communicationSettings = ApiCommunicationConfiguration.Kafka("userDeleted");
        }
        else
        {
            communicationSettings = ApiCommunicationConfiguration.Http;
        }

        serviceCollection.AddSingleton(communicationSettings);
        serviceCollection.AddCommunication(communicationSettings);

        return serviceCollection;
    }
}