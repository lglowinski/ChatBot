using ChatBot.Common.Communication.Kafka;
using ChatBot.Common.Communication.Serialization.Deserializers;
using Confluent.Kafka;

namespace ChatBot.Common.Communication.Serialization;

public class CustomSerializer<T> : ISerializer<T>, IDeserializer<T> where T : IKafkaMessage
{
    public byte[] Serialize(T data, SerializationContext context)
    {
        return data.Serialize();
    }

    public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        if (isNull)
            return default;
        
        return data.Deserialize<T>();
    }
}