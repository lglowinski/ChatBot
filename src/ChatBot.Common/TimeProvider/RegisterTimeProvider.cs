using Microsoft.Extensions.DependencyInjection;

namespace ChatBot.Common.TimeProvider;

public static class RegisterTimeProvider
{
    public static IServiceCollection AddDefaultTimeProvider(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ITimeProvider, SystemTimeProvider>();
        return serviceCollection;
    }
    
}