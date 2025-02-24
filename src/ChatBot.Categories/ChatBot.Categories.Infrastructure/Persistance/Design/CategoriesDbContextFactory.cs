using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ChatBot.Categories.Infrastructure.Persistance.Design;

public class CategoriesDbContextFactory : IDesignTimeDbContextFactory<CategoriesDbContext>
{
    public CategoriesDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CategoriesDbContext>();
        optionsBuilder.UseSqlServer();

        return new CategoriesDbContext(optionsBuilder.Options);
    }
}