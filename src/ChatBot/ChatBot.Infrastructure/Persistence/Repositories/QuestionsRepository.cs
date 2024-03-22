using ChatBot.Application;
using ChatBot.Domain;
using Microsoft.EntityFrameworkCore;

namespace ChatBot.Infrastructure.Persistence.Repositories;

public class QuestionsRepository(QuestionDbContext dbContext) : IQuestionsRepository
{
    public async Task<Question?> GetQuestionByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Questions.FirstOrDefaultAsync(q => q.Id == id, cancellationToken: cancellationToken);
    }
    public async Task AddQuestionAsync(Question question, CancellationToken cancellationToken = default)
    {
        await dbContext.Questions.AddAsync(question, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task UpdateQuestionAsync(Question question, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<Question>> SearchQuestions(string? searchTerm = null, string? orderBy = null, int count = 10,
        CancellationToken cancellationToken = default)
    {
        var questions = dbContext.Questions.AsQueryable();
        if (orderBy is not null)
            questions = questions.OrderByDescending(q => EF.Property<object>(q, orderBy));
        
        if(searchTerm is not null)
            questions = questions
                .Where(q => EF.Functions.Like(q.Title, $"%{searchTerm}%") 
                            || EF.Functions.Like(q.Summary, $"%{searchTerm}%"));
        
        return questions.Take(count).ToListAsync(cancellationToken);
    }

    public Task<List<Question>> ListLatestQuestionsAsync(string? searchTerm = null, int count = 10, CancellationToken cancellationToken = default)
    {
        return dbContext.Questions.OrderByDescending(q => q.CreatedAt).Take(count).ToListAsync(cancellationToken);
    }
}