using System.Runtime.Serialization;

namespace ChatBot.Contracts.Responses;

[DataContract]
public class ListQuestionsResponse(IEnumerable<Question> questions)
{
    [DataMember(Name = "questions")]
    public IEnumerable<Question> Questions { get; set; } = questions;
}

[DataContract]
public class Question(string id, string title, string summary, DateTime createdAt)
{
    [DataMember(Name = "id")]
    public string Id { get; set; } = id;

    [DataMember(Name = "title")]
    public string Title { get; set; } = title;

    [DataMember(Name = "summary")]
    public string Summary { get; set; } = summary;

    [DataMember(Name = "createdAt")]
    public DateTime CreatedAt { get; set; } = createdAt;
}