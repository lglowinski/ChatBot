using ChatBot.Common.Communication.Configuration;
using ChatBot.Common.Communication.Http.Strategies;
using ChatBot.Common.Communication.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChatBot.Common.Communication;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddCommunication(this IHostApplicationBuilder applicationBuilder, CommunicationConfiguration configuration)
    {
        if (configuration.CommunicationType is CommunicationType.Http)
            applicationBuilder.RegisterHttpCommunication(configuration);
        else
            applicationBuilder.RegisterKafkaCommunication(configuration);
        
        return applicationBuilder;
        
    }

    public static IHostApplicationBuilder RegisterHttpCommunication(this IHostApplicationBuilder applicationBuilder,
        CommunicationConfiguration configuration)
    {
        applicationBuilder.Services.AddHttpClient();
        applicationBuilder.Services.AddSingleton<HttpStrategyFactory>();
        applicationBuilder.Services.AddSingleton<IHttpSendStrategy, HttpDeleteStrategy>();
        applicationBuilder.Services.AddSingleton<IHttpSendStrategy, HttpPostStrategy>();
        applicationBuilder.Services.AddTransient<ICommunication, HttpCommunication>();
        return applicationBuilder;
    }
    
    public static IHostApplicationBuilder RegisterKafkaCommunication(this IHostApplicationBuilder applicationBuilder,
        CommunicationConfiguration configuration)
    {
        applicationBuilder.Services.AddSingleton<ICommunication, KafkaCommunication>();
        
        return applicationBuilder;
    }
}