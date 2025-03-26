using ChatBot.Common.Communication.Http.Strategies;

namespace ChatBot.Common.Communication.Http;

public interface IHttpMessage
{
    public HttpStrategyContext ToContext();
}