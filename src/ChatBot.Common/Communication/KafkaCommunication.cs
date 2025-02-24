using ChatBot.Common.Communication.Configuration;
using ChatBot.Common.Communication.Http;
using ChatBot.Common.Communication.Kafka;
using ChatBot.Common.TimeProvider;
using Confluent.Kafka;

namespace ChatBot.Common.Communication;

public class KafkaCommunication(
    IProducer<string, IKafkaMessage> producer,
    CommunicationConfiguration configuration,
    ITimeProvider timeProvider) : ICommunication
{
    public async Task SendAsync<T>(T request, CancellationToken cancellationToken = default) where T : IKafkaMessage, IHttpMessage
    {
        await SendAsyncInternal(request, cancellationToken);
    }

    public void Send<T>(T request) where T : IKafkaMessage, IHttpMessage
    {
        SendInternal(request);
    }

    private async Task SendAsyncInternal<T>(T request, CancellationToken cancellationToken = default)
        where T : IKafkaMessage
    {
        var kafkaConfiguration = GetKafkaConfiguration<T>();

        await producer.ProduceAsync(kafkaConfiguration.Topic, new Message<string, IKafkaMessage>
        {
            Value = request,
            Timestamp = new Timestamp(timeProvider.UtcNow)
        }, cancellationToken);
    }

    private void SendInternal<T>(T request)
        where T : IKafkaMessage
    {
        var kafkaConfiguration = GetKafkaConfiguration<T>();

        producer.Produce(kafkaConfiguration.Topic, new Message<string, IKafkaMessage>
        {
            Value = request,
            Timestamp = new Timestamp(timeProvider.UtcNow)
        });
    }
    
    private KafkaConfiguration GetKafkaConfiguration<T>()
        where T : IKafkaMessage
    {
        return configuration.AsKafkaConfiguration(typeof(T));
    }
}