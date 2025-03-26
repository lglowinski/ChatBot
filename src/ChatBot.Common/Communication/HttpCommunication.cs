using System.Text.Json;
using ChatBot.Common.Communication.Configuration;
using ChatBot.Common.Communication.Http;
using ChatBot.Common.Communication.Http.Strategies;
using ChatBot.Common.Communication.Kafka;

namespace ChatBot.Common.Communication;

public class HttpCommunication(IHttpClientFactory clientFactory, CommunicationConfiguration configuration, HttpStrategyFactory factory) : ICommunication
{
    public async Task SendAsync<T>(T request, CancellationToken cancellationToken = default) where T : IHttpMessage, IKafkaMessage
    {
        var httpConfiguration = GetHttpConfiguration<T>();
        using var client = clientFactory.CreateClient(httpConfiguration.ClientName);

        var strategyContext = request.ToContext();

        var strategy = factory.GetStrategy(strategyContext);
        
        if(strategy is null)
            throw new InvalidOperationException("No strategy found for the given context");
        
        await strategy.SendAsync();
    }

    public void Send<T>(T request) where T : IHttpMessage, IKafkaMessage<T>
    {
        throw new NotImplementedException();
    }
    
    public HttpConfiguration GetHttpConfiguration<T>()
    {
        return configuration.AsHttpConfiguration(typeof(T));
    }
}