using HttpMethod = ChatBot.Common.Communication.Configuration.HttpMethod;

namespace ChatBot.Common.Communication.Http.Strategies;

public class HttpPostStrategy(HttpClient client) : HttpSendStrategy(client)
{
    public override async Task SendAsync()
    {
        await client.PostAsync(Context.Uri, Context.Content);
    }

    public override bool IsApplicable(HttpStrategyContext? applicableContext)
    {
        if (applicableContext is null)
            return false;
        
        var isApplicable = applicableContext.Content is not null && applicableContext.Method is HttpMethod.Post;
        
        if (isApplicable)
            Context = applicableContext;

        return isApplicable;
    }
}