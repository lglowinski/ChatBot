using ChatBot.Application.Common;
using Microsoft.Extensions.DependencyInjection;

namespace ChatBot.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(options => 
            options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection)));

        services.AddCommon();
        
        return services;
    }
    
    private static IServiceCollection AddCommon(this IServiceCollection services)
    {
        services.AddScoped<IVerifier<IOrderable>, OrdererableVerifier>();

        return services;
    }
}