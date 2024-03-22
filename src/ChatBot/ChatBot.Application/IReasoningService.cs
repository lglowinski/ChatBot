using ChatBot.Application.Questions.Commands.AskQuestionCommand;

namespace ChatBot.Application;

public interface IReasoningService
{
    public Task<Answer> AskQuestionAsync(string question, CancellationToken cancellationToken = default);
}