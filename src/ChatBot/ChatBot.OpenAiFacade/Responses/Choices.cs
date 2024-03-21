using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ChatBot.OpenAiFacade.Responses;

[DataContract]
public class Choices
{
    [DataMember]
    [JsonPropertyName("index")]
    public int Index { get; set; }
    [DataMember]
    [JsonPropertyName("message")]
    public Message Message { get; set; }
    [DataMember]
    [JsonPropertyName("logprobs")]
    public object? Logprobs { get; set; }
    [DataMember]
    [JsonPropertyName("finish_reason")]
    public string FinishReason { get; set; }
}