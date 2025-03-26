using ChatBot.Domain;

namespace ChatBot.Application;

public interface IQuestionsRepository
{
    public Task<Question?> GetQuestionByIdAsync(string id, CancellationToken cancellationToken = default);
    public Task AddQuestionAsync(Question question, CancellationToken cancellationToken = default);
    public Task UpdateQuestionAsync(Question question, CancellationToken cancellationToken = default);
    public Task<List<Question>> SearchQuestions(string? searchTerm = null, string? orderBy = null, int count = 10,int page = 0, CancellationToken cancellationToken = default);
    public Task<List<string>> DeleteUserQuestionsAsync(string email, CancellationToken cancellationToken = default);
}