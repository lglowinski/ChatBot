using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ChatBot.OpenAiFacade.Responses;

[DataContract]
public class OpenAiResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("object")]
    public string @Object { get; set; }
    [JsonPropertyName("created")]
    public int Created { get; set; }
    [JsonPropertyName("model")]
    public string Model { get; set; }
    [JsonPropertyName("system_fingerprint")]
    public string SystemFingerprint { get; set; }
    [JsonPropertyName("choices")]
    public Choices[] Choices { get; set; }
    [JsonPropertyName("Usage")]
    public Usage Usage { get; set; }
}