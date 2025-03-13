using System.Text;
using ChatBot.Common.Communication.Http;
using ChatBot.Common.Communication.Http.Strategies;
using ChatBot.Common.Communication.Kafka;
using HttpMethod = ChatBot.Common.Communication.Configuration.HttpMethod;

namespace ChatBot.Common.Communication.Requests;

public record QuestionDeleted(string QuestionId) : IKafkaMessage, IHttpMessage
{
    public HttpStrategyContext ToContext()
    {
        return new HttpStrategyContext($"api/categories/{QuestionId}", null, HttpMethod.Delete);
    }

    public byte[] Serialize()
    {
        return Encoding.UTF8.GetBytes(QuestionId);
    }

    internal static QuestionDeleted Deserialize(byte[] data)
    {
        return new QuestionDeleted(Encoding.UTF8.GetString(data));
    }
}