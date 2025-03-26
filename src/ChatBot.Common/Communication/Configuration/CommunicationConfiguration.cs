namespace ChatBot.Common.Communication.Configuration;

public class CommunicationConfiguration
{
    /// <summary>
    /// Contains mappings between type of request and the endpoint/kafka topic
    /// </summary>
    public Dictionary<Type, IRequestConfiguration>? Mappings { get; init; }
    public CommunicationType CommunicationType { get; init; }
    
    public HttpConfiguration? AsHttpConfiguration(Type type)
    {
        EnsureMappings();
        
        return Mappings[type] as HttpConfiguration;
    }
    
    public KafkaConfiguration? AsKafkaConfiguration(Type type)
    {
        EnsureMappings();
        
        return Mappings![type] as KafkaConfiguration;
    }

    private void EnsureMappings()
    {
        if(Mappings is null)
            throw new InvalidOperationException("Mappings are not set");
    }
}

public enum CommunicationType
{
    Http,
    Kafka
}