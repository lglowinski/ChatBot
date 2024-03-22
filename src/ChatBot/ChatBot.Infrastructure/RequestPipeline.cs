using ChatBot.Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;

namespace ChatBot.Infrastructure;

public static class RequestPipeline
{
    public static IApplicationBuilder AddInfrastructureMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<TransactionMiddleware>();
        return app;
    }
}