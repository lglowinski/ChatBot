using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ChatBot.OpenAiFacade.Responses;

public class Usage
{
    [DataMember]
    [JsonPropertyName("prompt_tokens")]
    public int PromptTokens { get; set; }
    [DataMember]
    [JsonPropertyName("completion_tokens")]
    public int CompletionTokens { get; set; }
    [DataMember]
    [JsonPropertyName("total_tokens")]
    public int TotalTokens { get; set; }
}