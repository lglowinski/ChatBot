using ChatBot.Common.Communication.Configuration;
using ChatBot.Common.Communication.Http.Strategies;
using Microsoft.Extensions.DependencyInjection;

namespace ChatBot.Common.Communication;

public static class DependencyInjection
{
    public static IServiceCollection AddCommunication(this IServiceCollection services, CommunicationConfiguration configuration)
    {
        if (configuration.CommunicationType is CommunicationType.Http)
            services.RegisterHttpCommunication();
        else
            services.RegisterKafkaCommunication();
        
        return services;
        
    }
    
    public static IServiceCollection RegisterHttpCommunication(this IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddSingleton<HttpStrategyFactory>();
        services.AddSingleton<IHttpSendStrategy, HttpDeleteStrategy>();
        services.AddSingleton<IHttpSendStrategy, HttpPostStrategy>();
        services.AddTransient<ICommunication, HttpCommunication>();
        return services;
    }
    
    public static IServiceCollection RegisterKafkaCommunication(this IServiceCollection services)
    {
        services.AddSingleton<ICommunication, KafkaCommunication>();
        return services;
    }
}