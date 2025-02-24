namespace ChatBot.Common.Communication.Http.Strategies;

public interface IHttpSendStrategy
{
    public Task SendAsync();
    public bool IsApplicable(HttpStrategyContext? applicableContext);
}