using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ChatBot.Users.Infrastructure.Persistance.Design;

public class UsersDbContextFactory : IDesignTimeDbContextFactory<UsersDbContext>
{
    public UsersDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<UsersDbContext>();
        optionsBuilder.UseSqlServer();

        return new UsersDbContext(optionsBuilder.Options);
    }
}