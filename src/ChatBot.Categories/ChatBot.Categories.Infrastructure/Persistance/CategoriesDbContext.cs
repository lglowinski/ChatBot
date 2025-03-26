using System.Reflection;
using ChatBot.Categories.Domain;
using Microsoft.EntityFrameworkCore;

namespace ChatBot.Categories.Infrastructure.Persistance;

public class CategoriesDbContext(DbContextOptions<CategoriesDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories { get; init; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}