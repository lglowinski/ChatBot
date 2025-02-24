using ChatBot.Common.Communication.Http;
using ChatBot.Common.Communication.Kafka;

namespace ChatBot.Common.Communication;

public interface ICommunication
{
    public Task SendAsync<T>(T request, CancellationToken cancellationToken = default) where T : IHttpMessage, IKafkaMessage;
    public void Send<T>(T request)  where T : IHttpMessage, IKafkaMessage;
}