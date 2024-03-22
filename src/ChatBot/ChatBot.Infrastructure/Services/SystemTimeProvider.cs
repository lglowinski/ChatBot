using ChatBot.Domain;

namespace ChatBot.Infrastructure.Services;

public class SystemTimeProvider : ITimeProvider
{
    public DateTime Now => DateTime.Now;
    public DateTime UtcNow => DateTime.UtcNow;
}