namespace ChatBot.Common.Communication.Kafka;

public interface IKafkaMessage
{
    public byte[] Serialize();
}

public interface IKafkaMessage<out T> : IKafkaMessage  where T : IKafkaMessage<T>
{
    
}