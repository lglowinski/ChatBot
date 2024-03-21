using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ChatBot.OpenAiFacade.Responses;

[DataContract]
public class Message
{
    [DataMember]
    [JsonPropertyName("role")]
    public string Role { get; set; }
    [DataMember]
    [JsonPropertyName("content")]
    public string Content { get; set; }
}