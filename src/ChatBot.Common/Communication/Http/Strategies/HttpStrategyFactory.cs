namespace ChatBot.Common.Communication.Http.Strategies;

public class HttpStrategyFactory(IEnumerable<IHttpSendStrategy> strategies)
{
    public IHttpSendStrategy? GetStrategy(HttpStrategyContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        
        return strategies.FirstOrDefault(strategy => strategy.IsApplicable(context));
    }
}