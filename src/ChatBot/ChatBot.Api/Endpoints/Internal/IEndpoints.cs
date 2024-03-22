using ChatBot.Api.Settings;

namespace ChatBot.Api.Endpoints.Internal;

public interface IEndpoints
{
    public static abstract void DefineEndpoints(IEndpointRouteBuilder app, RateLimitingSettings rateLimitingSettings);
    public static abstract void AddService(IServiceCollection services, IConfiguration configuration);
}