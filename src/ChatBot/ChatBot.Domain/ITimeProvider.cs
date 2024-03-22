namespace ChatBot.Domain;

public interface ITimeProvider
{
    public DateTime Now { get; }
    public DateTime UtcNow { get; }
}