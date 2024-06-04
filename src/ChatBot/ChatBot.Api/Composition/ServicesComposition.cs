using AspireOrchestrator.ServiceDefaults;
using ChatBot.Api.Endpoints.Internal;
using ChatBot.Api.Settings;
using ChatBot.Application;
using ChatBot.Infrastructure;
using ChatBot.OpenAiFacade;
using Microsoft.AspNetCore.RateLimiting;

namespace ChatBot.Api.Composition;

public static class ServicesComposition
{
    public static IHostApplicationBuilder RegisterDependencies(this IHostApplicationBuilder builder)
    {
        builder.AddServiceDefaults();
        builder.RegisterInfrastructure("avatarui");

        builder.Services.RegisterServices(builder.Configuration);

        return builder;
    }
    
    public static IServiceCollection RegisterServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var parameters = configuration.GetSection("Parameters");
        
        var openAiSettings = new OpenAiSettings();
        parameters.GetSection(nameof(OpenAiSettings)).Bind(openAiSettings);
        
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

        return serviceCollection;
    }
    
    public static IServiceCollection AddRateLimiting(this IServiceCollection serviceCollection,
        RateLimitingSettings settings)
    {
        serviceCollection.AddRateLimiter(
            o => o.AddFixedWindowLimiter(settings.PolicyName, options =>
            {
                options.PermitLimit = settings.PermitLimit;
                options.Window = TimeSpan.FromMinutes(settings.WindowTimeInMinutes);
            }));

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