using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ChatBot.OpenAiFacade.Requests;

[DataContract]
public class Message(string role, string content)
{
    [DataMember]
    [JsonPropertyName("role")]
    public string Role { get; } = role;

    [DataMember]
    [JsonPropertyName("content")]
    public string Content { get; } = content;
}