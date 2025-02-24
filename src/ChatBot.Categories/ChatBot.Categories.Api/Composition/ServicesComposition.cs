using AspireOrchestrator.ServiceDefaults;
using ChatBot.Categories.Infrastructure;
using ChatBot.Common.Endpoints;
using ChatBot.Common.RateLimiting;
using ChatBot.Common.TimeProvider;

namespace ChatBot.Categories.Api.Composition;

public static class ServicesComposition
{
    public static IHostApplicationBuilder RegisterDependencies(this IHostApplicationBuilder builder, ILogger logger)
    {
        builder.AddServiceDefaults();
        builder.AddInfrastructure("categories");
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDefaultTimeProvider();
        builder.Services.AddEndpoints<Program>(builder.Configuration);
        
        builder.Services.RegisterServices(builder.Configuration, logger);

        return builder;
    }

    public static IServiceCollection RegisterServices(this IServiceCollection serviceCollection, IConfiguration configuration, ILogger logger)
    {
        
        var rateLimitingSettings = new RateLimitingSettings();
        configuration.GetSection(nameof(RateLimitingSettings)).Bind(rateLimitingSettings);
        
        serviceCollection.AddSingleton(rateLimitingSettings);

        serviceCollection.AddRateLimiting(rateLimitingSettings);
        
        serviceCollection.AddCors();
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