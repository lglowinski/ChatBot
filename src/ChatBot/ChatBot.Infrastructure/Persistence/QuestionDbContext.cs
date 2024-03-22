using System.Reflection;
using ChatBot.Domain;
using Microsoft.EntityFrameworkCore;

namespace ChatBot.Infrastructure.Persistence;

public class QuestionDbContext(DbContextOptions<QuestionDbContext> options) : DbContext(options)
{
    public DbSet<Question> Questions { get; init; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}