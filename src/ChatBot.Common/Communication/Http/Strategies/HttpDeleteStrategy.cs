using HttpMethod = ChatBot.Common.Communication.Configuration.HttpMethod;

namespace ChatBot.Common.Communication.Http.Strategies;

public class HttpDeleteStrategy(HttpClient client) : HttpSendStrategy(client)
{
    public override async Task SendAsync()
    {
        await Client.DeleteAsync(Context.Uri);
    }

    public override bool IsApplicable(HttpStrategyContext? applicableContext)
    {
        if (applicableContext is null)
            return false;
        
        var isApplicable = applicableContext is { Method: HttpMethod.Delete, Content: null };
        
        if (isApplicable)
            Context = applicableContext;

        return isApplicable;
    }
}