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
    public int HelpfulCount { get; set; }
    public string AuthorEmail { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    
    public Question(
        string id,
        string title,
        string answer,
        List<string> tags,
        DateTimeOffset createdAt,
        string summary,
        string authorEmail,
        bool isDeleted = false,
        DateTimeOffset? deletedAt = null)
    {
        Id = id;
        Title = title;
        Answer = answer;
        Tags = tags;
        CreatedAt = createdAt;
        Summary = summary;
        Downvotes = 0;
        Upvotes = 0;
        HelpfulCount = 0;
        AuthorEmail = authorEmail;
        IsDeleted = isDeleted;
        DeletedAt = deletedAt;
    }

    [JsonConstructor]
    public Question(
        string id,
        string title,
        string answer,
        int upvotes,
        int downvotes,
        List<string> tags,
        int helpfulCount,
        DateTimeOffset createdAt,
        string summary, string authorEmail) : this(id, title, answer, tags, createdAt, summary, authorEmail)
    {
        Upvotes = upvotes;
        Downvotes = downvotes;
        HelpfulCount = helpfulCount;
    }

    public void Upvote()
    {
        Upvotes++;
    }

    public void Downvote()
    {
        Downvotes++;
    }

    public void Helped()
    {
        HelpfulCount++;
    }
}