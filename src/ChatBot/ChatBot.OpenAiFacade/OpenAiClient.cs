using System.Text;
using System.Text.Json;
using ChatBot.Application;
using ChatBot.Application.Questions.Commands.AskQuestionCommand;
using ChatBot.Domain;
using ChatBot.OpenAiFacade.Requests;
using ChatBot.OpenAiFacade.Responses;

namespace ChatBot.OpenAiFacade;

public class OpenAiClient : HttpClient, IReasoningService
{
    public async Task<Answer> AskQuestionAsync(string question, CancellationToken cancellationToken = default)
    {
        var request = new OpenAiRequestBuilder()
            .WithModel()
            .WithSystemDefinitions([
                SystemDefinitions.Default, 
                SystemDefinitions.TagsDefinition, 
                SystemDefinitions.SummaryDefinition, 
                SystemDefinitions.ResponseDefinition])
            .WithPrompt(question)
            .Build();

        var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

        var response = await PostAsync("completions", content, cancellationToken);

        response.EnsureSuccessStatusCode();

        var openAiResponse =
            await JsonSerializer.DeserializeAsync<OpenAiResponse>(
                await response.Content.ReadAsStreamAsync(cancellationToken), cancellationToken: cancellationToken);
        
        return JsonSerializer.Deserialize<Answer>(openAiResponse.Choices[0].Message.Content);
    }
}