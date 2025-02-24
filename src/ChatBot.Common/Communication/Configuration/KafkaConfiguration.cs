namespace ChatBot.Common.Communication.Configuration;

public class KafkaConfiguration : IRequestConfiguration
{
    public string Topic { get; set; }
}