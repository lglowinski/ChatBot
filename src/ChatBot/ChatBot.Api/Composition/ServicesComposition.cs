using AspireOrchestrator.ServiceDefaults;
using ChatBot.Api.Settings;
using ChatBot.Application;
using ChatBot.Common.Auth;
using ChatBot.Common.Endpoints;
using ChatBot.Common.RateLimiting;
using ChatBot.Common.TimeProvider;
using ChatBot.Infrastructure;
using ChatBot.OpenAiFacade;

namespace ChatBot.Api.Composition;

public static class ServicesComposition
{
    public static IHostApplicationBuilder RegisterDependencies(this IHostApplicationBuilder builder, ILogger logger)
    {
        builder.AddServiceDefaults();
        builder.AddInfrastructure("avatarui");

        builder.Services.RegisterServices(builder.Configuration, logger);

        return builder;
    }
    
    public static IServiceCollection RegisterServices(this IServiceCollection serviceCollection, IConfiguration configuration, ILogger logger)
    {
        var openAiSettings = new OpenAiSettings();
        configuration.GetSection(nameof(OpenAiSettings)).Bind(openAiSettings);
        
        var rateLimitingSettings = new RateLimitingSettings();
        configuration.GetSection(nameof(RateLimitingSettings)).Bind(rateLimitingSettings);
        
        serviceCollection.AddSingleton(rateLimitingSettings);
        
        serviceCollection.AddApplication();
        serviceCollection.AddEndpointsApiExplorer();
        serviceCollection.AddSwaggerGen();
        serviceCollection.AddEndpoints<Program>(configuration);
        serviceCollection.AddOpenAiClient(openAiSettings.Url, openAiSettings.ApiKey);
        serviceCollection.AddRateLimiting(rateLimitingSettings);
        serviceCollection.AddCors();
        serviceCollection.AddAuthentication(configuration, logger);
        serviceCollection.AddAuthorization();
        serviceCollection.AddDefaultTimeProvider();

        return serviceCollection;
    }
    
    public static IServiceCollection AddCors(this IServiceCollection serviceCollection)
    {
        //TODO : Obtain configuration from appsettings.json
        return serviceCollection.AddCors(options =>
        {
            options.AddDefaultPolicy(p =>
                p
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
    }


}