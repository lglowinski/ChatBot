namespace ChatBot.Domain;

public interface IReasoningService
{
    public Task<Answer> AskQuestionAsync(Question question, CancellationToken cancellationToken = default);
}