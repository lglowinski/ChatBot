using System.Reflection;
using System.Text.Json;
using ChatBot.Domain;

namespace ChatBot.MigrationService;

public class SeedDataLoader(ILogger<SeedDataLoader> logger)
{
    public async Task<IEnumerable<Question>> GetData(CancellationToken cancellationToken)
    {
        var assembly = Assembly.GetExecutingAssembly();
        const string resourceName = "ChatBot.MigrationService.seed.data.json";

        await using var stream = assembly.GetManifestResourceStream(resourceName);

        if (stream is null)
        {
            logger.LogError("Resource not found: {ResourceName}", resourceName);
            return Enumerable.Empty<Question>();
        }
        
        var result = await JsonSerializer.DeserializeAsync<List<Question>>(stream, cancellationToken: cancellationToken);

        if (result is not null) 
            return result;
        
        logger.LogError("Failed to parse json");
        return Enumerable.Empty<Question>();
    }
}