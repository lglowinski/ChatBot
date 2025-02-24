using ChatBot.Categories.Domain;

namespace ChatBot.Categories.Application;

public interface ICategoriesRepository
{
    public Task<Category?> GetCategoryByIdAsync(string id, CancellationToken cancellationToken = default);
    public Task AddCategoryAsync(Category category, CancellationToken cancellationToken = default);
    public Task DeleteCategoryByNameAsync(string name, CancellationToken cancellationToken = default);
    public Task DeleteCategoryByQuestionId(string questionId, CancellationToken cancellationToken = default);
}