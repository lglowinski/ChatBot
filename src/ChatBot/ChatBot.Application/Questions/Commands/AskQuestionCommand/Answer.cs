using System.Text.Json.Serialization;

namespace ChatBot.Application.Questions.Commands.AskQuestionCommand;

[method: JsonConstructor]
public class Answer(string value, IEnumerable<string> tags, string summary)
{
    [JsonPropertyName("answer")]
    public string Value { get; } = value;

    [JsonPropertyName("tags")]
    public IEnumerable<string> Tags { get; } = tags;

    [JsonPropertyName("summary")]
    public string Summary { get; } = summary;
}