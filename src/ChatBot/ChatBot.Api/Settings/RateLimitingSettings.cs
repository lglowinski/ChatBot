using System.Threading.RateLimiting;

namespace ChatBot.Api.Settings;

public class RateLimitingSettings
{
    public string PolicyName { get; init; } = null!;
    public int WindowTimeInMinutes { get; init; }
    public int PermitLimit { get; init; }
}