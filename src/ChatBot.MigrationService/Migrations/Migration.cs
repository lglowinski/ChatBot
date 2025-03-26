using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using OpenTelemetry.Trace;

namespace ChatBot.MigrationService.Migrations;

public interface IMigration
{
    public Task ExecuteAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken = default);
}

public class Migration<T> : IMigration where T : DbContext
{
    private static readonly ActivitySource _activitySource = new(MigrationConsts.ActivitySourceName);
    
    public async Task ExecuteAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken = default)
    {
        using var activity = _activitySource.StartActivity($"Migrating {typeof(T).Name} database context", ActivityKind.Client);

        try
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<T>();

            await EnsureDatabaseAsync(dbContext, cancellationToken);
            await MigrateAsync(dbContext, cancellationToken);
        }
        catch (Exception ex)
        {
            activity?.RecordException(ex);
            throw;
        }
    }

    private static async Task EnsureDatabaseAsync(T context, CancellationToken cancellationToken = default)
    {
        var dbCreator = context.GetService<IRelationalDatabaseCreator>();

        var strategy = context.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            if (!await dbCreator.ExistsAsync(cancellationToken))
            {
                await dbCreator.CreateAsync(cancellationToken);
            }
        });
    }
    private static async Task MigrateAsync(T context, CancellationToken cancellationToken = default)
    {
        var strategy = context.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            
            await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
            await context.Database.MigrateAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

        });
    }
}