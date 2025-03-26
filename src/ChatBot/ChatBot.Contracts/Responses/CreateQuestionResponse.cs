using System.Runtime.Serialization;

namespace ChatBot.Contracts.Responses;

[DataContract]
public record CreateQuestionResponse([property: DataMember(Name = "id")] string Id, [property: DataMember(Name = "title")] string Title,
    [property: DataMember(Name = "answer")]
    string Answer,
    [property: DataMember(Name = "upvotes")]
    int Upvotes,
    [property: DataMember(Name = "downvotes")]
    int Downvotes,
    [property:DataMember(Name="authorEmail")]
    string AuthorEmail);