using System.Text;
using ChatBot.Common.Communication.Http;
using ChatBot.Common.Communication.Http.Strategies;
using ChatBot.Common.Communication.Kafka;
using HttpMethod = ChatBot.Common.Communication.Configuration.HttpMethod;

namespace ChatBot.Common.Communication.Requests;

public record UserDeleted(string UserEmail) : IKafkaMessage<UserDeleted>, IHttpMessage
{
    public HttpStrategyContext ToContext()
    {
        return new HttpStrategyContext($"api/categories/{UserEmail}", null, HttpMethod.Delete);
    }

    public byte[] Serialize()
    {
        return Encoding.UTF8.GetBytes(UserEmail);
    }

    internal static UserDeleted Deserialize(byte[] data)
    {
        return new UserDeleted(Encoding.UTF8.GetString(data));
    }
}