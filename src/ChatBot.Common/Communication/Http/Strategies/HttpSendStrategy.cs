namespace ChatBot.Common.Communication.Http.Strategies;

public abstract class HttpSendStrategy(HttpClient client) : IHttpSendStrategy
{
    protected HttpStrategyContext Context { get; set; }
    protected HttpClient Client => client;
    
    public abstract Task SendAsync();

    public abstract bool IsApplicable(HttpStrategyContext? applicableContext);
}