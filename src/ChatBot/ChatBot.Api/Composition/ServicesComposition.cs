using AspireOrchestrator.ServiceDefaults;
using ChatBot.Api.Settings;
using ChatBot.Api.Workers;
using ChatBot.Application;
using ChatBot.Common.Auth;
using ChatBot.Common.Communication;
using ChatBot.Common.Communication.Configuration;
using ChatBot.Common.Communication.Kafka;
using ChatBot.Common.Communication.Requests;
using ChatBot.Common.Communication.Serialization;
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

        builder.AddKafkaProducer<string, IKafkaMessage>("messaging", static settings => settings.DisableHealthChecks = true,
            static provider =>
            {
                var serializer = new CustomSerializer<IKafkaMessage>();
                provider.SetValueSerializer(serializer);
            });
        builder.AddKafkaConsumer<string, UserDeleted>("messaging", settings =>
        {
            settings.Config.GroupId = "userDeleted";
            settings.Config.AllowAutoCreateTopics = true;
        }, static builder =>
        {
            var deserializer = new CustomSerializer<UserDeleted>();
            builder.SetValueDeserializer(deserializer);
        });
        builder.AddCommunication(builder.Configuration, logger);
        builder.Services.AddHostedService<KafkaWorker<UserDeleted>>();
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

    private static IHostApplicationBuilder AddCommunication(this IHostApplicationBuilder applicationBuilder, IConfiguration configuration, ILogger logger)
    {
        var useKafka = configuration.GetValue<bool>("UseKafka");
        CommunicationConfiguration communicationSettings;
        
        if (useKafka)
        {
            communicationSettings = ApiCommunicationConfiguration.Kafka("questionDeleted");
        }
        else
        {
            communicationSettings = ApiCommunicationConfiguration.Http;
        }

        applicationBuilder.Services.AddSingleton(communicationSettings);
        applicationBuilder.AddCommunication(communicationSettings);

        return applicationBuilder;
    }
}