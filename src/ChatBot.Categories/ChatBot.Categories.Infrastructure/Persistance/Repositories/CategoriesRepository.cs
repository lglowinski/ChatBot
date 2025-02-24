using ChatBot.Categories.Application;
using ChatBot.Categories.Domain;
using ChatBot.Common.TimeProvider;
using Microsoft.EntityFrameworkCore;

namespace ChatBot.Categories.Infrastructure.Persistance.Repositories;

public class CategoriesRepository(CategoriesDbContext context, ITimeProvider timeProvider) : ICategoriesRepository
{
    public async Task<Category?> GetCategoryByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await context.Categories.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task AddCategoryAsync(Category category, CancellationToken cancellationToken = default)
    {
        await context.Categories.AddAsync(category, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteCategoryByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        await context.Categories.Where(category => category.Name == name).ExecuteUpdateAsync(setters =>
                setters.SetProperty(c => c.IsDeleted, true).SetProperty(c => c.DeletedAt, timeProvider.UtcNow),
            cancellationToken);
    }
    
    public async Task DeleteCategoryByQuestionId(string questionId, CancellationToken cancellationToken = default)
    {
        var categories = context.Categories.Where(c => c.QuestionsIds.Contains(questionId));

        foreach (var category in categories)
        {
            category.QuestionsIds.Remove(questionId);
            if(category.QuestionsIds.Count == 0)
            {
                category.IsDeleted = true;
                category.DeletedAt = timeProvider.UtcNow;
            }

            context.Categories.Update(category);
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}