using ChatBot.Common.TimeProvider;

namespace ChatBot.Categories.Domain;

public class Category
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int QuestionCount => QuestionsIds.Count;
    public bool IsDeleted { get; set; }
    public HashSet<string> QuestionsIds { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }

    public Category(string id, string name, HashSet<string> questionsIds, bool isDeleted = false, DateTimeOffset? deletedAt = null)
    {
        Id = id;
        Name = name;
        QuestionsIds = questionsIds;
        IsDeleted = isDeleted;
        DeletedAt = deletedAt;
    }

    public void DeleteQuestion(string questionId, ITimeProvider timeProvider)
    {
        QuestionsIds.Remove(questionId);
        
        if (QuestionCount != 0)
            return;
        
        IsDeleted = true;
        DeletedAt = timeProvider.UtcNow;
    }
}