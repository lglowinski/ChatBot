using ChatBot.Common.RateLimiting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChatBot.Common.Endpoints;

public interface IEndpoints
{
    public static abstract void DefineEndpoints(IEndpointRouteBuilder app, RateLimitingSettings rateLimitingSettings = null);
    public static abstract void AddService(IServiceCollection services, IConfiguration configuration);
}