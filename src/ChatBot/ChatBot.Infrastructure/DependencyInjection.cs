using ChatBot.Application;
using ChatBot.Domain;
using ChatBot.Infrastructure.Persistence;
using ChatBot.Infrastructure.Persistence.Repositories;
using ChatBot.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChatBot.Infrastructure;

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
        builder.AddSqlServerDbContext<QuestionDbContext>(db, configureDbContextOptions: options =>
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
        builder.Services.AddScoped<ITimeProvider, SystemTimeProvider>();

        builder.AddRepositories();
        
        return builder;
    }

    private static IHostApplicationBuilder AddRepositories(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<IQuestionsRepository, QuestionsRepository>();

        return builder;
    }
}