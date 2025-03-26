namespace ChatBot.Common.TimeProvider;

public class SystemTimeProvider : ITimeProvider
{
    public DateTimeOffset Now => DateTime.Now;
    public DateTimeOffset UtcNow => DateTime.UtcNow;
    public TimeSpan UtcNowTimeSpan => new TimeSpan(UtcNow.Ticks);
}