using System.Net.Http.Headers;
using ChatBot.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace ChatBot.OpenAiFacade;

public static class DependencyInjection
{
    public static IServiceCollection AddOpenAiClient(this IServiceCollection services, string url,
        string apiKey)
    {
        services.AddHttpClient<IReasoningService, OpenAiClient>(_ =>
        {
            var client = new OpenAiClient();

            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        });

        return services;
    }
}