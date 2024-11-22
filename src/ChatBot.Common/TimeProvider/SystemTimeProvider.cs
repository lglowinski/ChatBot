namespace ChatBot.Common.TimeProvider;

public class SystemTimeProvider : ITimeProvider
{
    public DateTimeOffset Now => DateTime.Now;
    public DateTimeOffset UtcNow => DateTime.UtcNow;
}