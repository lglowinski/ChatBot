using ChatBot.Categories.Application;
using ChatBot.Categories.Infrastructure.Persistance;
using ChatBot.Categories.Infrastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChatBot.Categories.Infrastructure;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddInfrastructure(this IHostApplicationBuilder builder, string db)
    {
        return builder
            .AddDbContext(db)
            .AddPersistence();
    }

    private static IHostApplicationBuilder AddDbContext(this IHostApplicationBuilder builder, string db)
    {
        builder.AddSqlServerDbContext<CategoriesDbContext>(db, configureDbContextOptions: options =>
        {
            options.UseSqlServer(sqlServerOptions =>
            {
                sqlServerOptions.MigrationsAssembly(typeof(DependencyInjection).Assembly.GetName().Name);
            });
        });

        return builder;
    }

    private static IHostApplicationBuilder AddPersistence(this IHostApplicationBuilder builder)
    {
        builder.AddRepositories();
        
        return builder;
    }

    private static IHostApplicationBuilder AddRepositories(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();

        return builder;
    }
}