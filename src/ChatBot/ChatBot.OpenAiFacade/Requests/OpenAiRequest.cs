using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ChatBot.OpenAiFacade.Requests;

[DataContract]
public class OpenAiRequest(string model, IEnumerable<Message> messages)
{
    [DataMember]
    [JsonPropertyName("model")]
    public string Model { get; } = model;

    [DataMember]
    [JsonPropertyName("messages")]
    public IEnumerable<Message> Messages { get; } = messages;
}