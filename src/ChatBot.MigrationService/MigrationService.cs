using ChatBot.MigrationService.Migrations;

namespace ChatBot.MigrationService;

public class MigrationService(
    ILogger<MigrationService> logger,
    IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime,
    IEnumerable<IMigration> migrations)
    : BackgroundService
{
    public const string ActivitySourceName = "Migrations";

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        foreach (var migration in migrations)
        {
            await migration.ExecuteAsync(serviceProvider, cancellationToken);
        }
        
        hostApplicationLifetime.StopApplication();
    }
}