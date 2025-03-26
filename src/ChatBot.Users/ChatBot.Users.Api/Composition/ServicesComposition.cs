using System.Text.Json;
using ChatBot.Common.Auth;
using ChatBot.Common.Communication;
using ChatBot.Common.Communication.Configuration;
using ChatBot.Common.Communication.Kafka;
using ChatBot.Common.Communication.Requests;
using ChatBot.Common.Communication.Serialization;
using ChatBot.Common.Endpoints;
using ChatBot.Common.RateLimiting;
using ChatBot.Common.TimeProvider;
using ChatBot.Users.Application;
using ChatBot.Users.Infrastructure;
using ChatBot.Users.Settings;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;

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
        builder.AddCommunication<UserDeleted>(builder.Configuration, logger);

        return builder;
    }

    private static IHostApplicationBuilder AddCommunication<T>(this IHostApplicationBuilder builder,
        IConfiguration configuration, ILogger logger)
    {
        var useKafka = configuration.GetValue<bool>("UseKafka");
        CommunicationConfiguration communicationSettings;

        if (useKafka)
        {
            communicationSettings = ApiCommunicationConfiguration.Kafka("userDeleted");
            builder.AddKafkaProducer<string, IKafkaMessage>("messaging", static settings => settings.DisableHealthChecks = true,
                static provider =>
                {
                    var serializer = new CustomSerializer<IKafkaMessage>();
                    provider.SetValueSerializer(serializer);
                });
        }
        else
        {
            communicationSettings = ApiCommunicationConfiguration.Http;
        }

        builder.Services.AddSingleton(communicationSettings);
        builder.AddCommunication(communicationSettings);

        return builder;
    }
}