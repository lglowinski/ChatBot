using HttpMethod = ChatBot.Common.Communication.Configuration.HttpMethod;

namespace ChatBot.Common.Communication.Http.Strategies;

public record HttpStrategyContext(string Uri, StringContent? Content, HttpMethod Method);