using ChatBot.Common.Communication.Configuration;
using ChatBot.Common.Communication.Requests;
using HttpMethod = ChatBot.Common.Communication.Configuration.HttpMethod;

namespace ChatBot.Users.Settings;

public static class ApiCommunicationConfiguration
{
    public static CommunicationConfiguration Http => new CommunicationConfiguration
    {
        Mappings = new Dictionary<Type, IRequestConfiguration>()
        {
            [typeof(UserDeleted)] = new HttpConfiguration
            {
                BaseUri = new Uri("https://api"),
                ClientName = "QuestionsClient",
                Method = HttpMethod.Delete
            }
        },
        CommunicationType = CommunicationType.Http
    };
    
    public static CommunicationConfiguration Kafka(string topic) => new CommunicationConfiguration
    {
        Mappings = new Dictionary<Type, IRequestConfiguration>()
        {
            [typeof(UserDeleted)] = new KafkaConfiguration()
            {
                Topic = topic
            }
        },
        CommunicationType = CommunicationType.Kafka
    };
}