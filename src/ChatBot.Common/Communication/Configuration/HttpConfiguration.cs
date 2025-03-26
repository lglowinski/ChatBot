namespace ChatBot.Common.Communication.Configuration;

public class HttpConfiguration : IRequestConfiguration
{
    public string ClientName { get; set; }
    public HttpMethod Method { get; set; }
    public Uri BaseUri { get; set; }
}

public enum HttpMethod
{
    Get,
    Post,
    Put,
    Delete
}