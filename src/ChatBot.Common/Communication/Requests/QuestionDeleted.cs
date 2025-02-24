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
}