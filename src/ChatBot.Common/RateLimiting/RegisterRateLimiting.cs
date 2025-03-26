using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.DependencyInjection;

namespace ChatBot.Common.RateLimiting;

public static class RegisterRateLimiting
{
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
}