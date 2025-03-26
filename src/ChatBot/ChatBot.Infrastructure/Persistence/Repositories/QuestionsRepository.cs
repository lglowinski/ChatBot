using ChatBot.Application;
using ChatBot.Common.TimeProvider;
using ChatBot.Domain;
using Microsoft.EntityFrameworkCore;

namespace ChatBot.Infrastructure.Persistence.Repositories;

public class QuestionsRepository(QuestionDbContext dbContext, ITimeProvider timeProvider) : IQuestionsRepository
{
    public async Task<Question?> GetQuestionByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await dbContext
            .Questions
            .FirstOrDefaultAsync(q => q.Id == id, cancellationToken: cancellationToken);
    }

    public async Task AddQuestionAsync(Question question, CancellationToken cancellationToken = default)
    {
        await dbContext.Questions.AddAsync(question, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateQuestionAsync(Question question, CancellationToken cancellationToken = default)
    {
        dbContext.Questions.Update(question);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<List<Question>> SearchQuestions(string? searchTerm = null, string? orderBy = null, int count = 10,
        int page = 0,
        CancellationToken cancellationToken = default)
    {
        var questions = dbContext.Questions.AsQueryable();
        if (orderBy is not null)
            questions = questions.OrderByDescending(q => EF.Property<object>(q, orderBy));

        if (searchTerm is not null)
        {
            questions = questions
                .Where(q =>
                    EF.Functions.Like(q.Title, $"%{searchTerm}%")
                    || EF.Functions.Like(q.Summary, $"%{searchTerm}%")
                    || q.Tags.Any(t => EF.Functions.Like(t, $"%{searchTerm}%")));
        }

        return questions.Skip(page * count).Take(count).ToListAsync(cancellationToken);
    }

    public async Task<List<string>> DeleteUserQuestionsAsync(string email,
        CancellationToken cancellationToken = default)
    {
        var deletedQuestionsIds = await dbContext.Questions.Where(q => q.AuthorEmail == email).Select(q => q.Id)
            .ToListAsync(cancellationToken);

        await dbContext.Questions.Where(q => q.AuthorEmail == email)
            .ExecuteUpdateAsync(setters =>
                    setters
                        .SetProperty(c => c.IsDeleted, true)
                        .SetProperty(c => c.DeletedAt, timeProvider.UtcNow),
                cancellationToken);

        return deletedQuestionsIds;
    }
}