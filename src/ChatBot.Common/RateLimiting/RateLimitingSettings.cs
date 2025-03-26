namespace ChatBot.Common.RateLimiting;

public class RateLimitingSettings
{
    public string PolicyName { get; init; } = null!;
    public int WindowTimeInMinutes { get; init; }
    public int PermitLimit { get; init; }
}