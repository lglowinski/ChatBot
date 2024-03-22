using System.Text.Json.Serialization;

namespace ChatBot.Domain;

public class Question
{
    public string Id { get; set; }
    public string Title { get; init; }
    public string Answer { get; init; }
    public int Upvotes { get; set; }
    public int Downvotes { get; set; }
    public List<string> Tags { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public string Summary { get; init; }
    
    public Question(
        string id,
        string title,
        string answer,
        List<string> tags,
        DateTimeOffset createdAt,
        string summary)
    {
        Id = id;
        Title = title;
        Answer = answer;
        Tags = tags;
        CreatedAt = createdAt;
        Summary = summary;
        Downvotes = 0;
        Upvotes = 0;
    }

    [JsonConstructor]
    public Question(
        string id,
        string title,
        string answer,
        int upvotes,
        int downvotes,
        List<string> tags,
        DateTimeOffset createdAt,
        string summary) : this(id, title, answer, tags, createdAt, summary)
    {
        Upvotes = upvotes;
        Downvotes = downvotes;
    }

    public void Upvote()
    {
        Upvotes++;
    }

    public void Downvote()
    {
        Downvotes++;
    }
}