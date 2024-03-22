using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ChatBot.Infrastructure.Persistence.Design;

public class QuestionDbContextFactory : IDesignTimeDbContextFactory<QuestionDbContext>
{
    public QuestionDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<QuestionDbContext>();
        optionsBuilder.UseSqlServer();

        return new QuestionDbContext(optionsBuilder.Options);
    }
}