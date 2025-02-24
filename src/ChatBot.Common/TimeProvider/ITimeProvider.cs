namespace ChatBot.Common.TimeProvider;

public interface ITimeProvider
{
    public DateTimeOffset Now { get; }
    public DateTimeOffset UtcNow { get; }
    public TimeSpan UtcNowTimeSpan { get; }
}