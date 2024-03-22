using System.Runtime.Serialization;

namespace ChatBot.Contracts.Responses;

[DataContract]
public record GetQuestionDetailsResponse([property: DataMember(Name = "title")] string Title,
    [property: DataMember(Name = "answer")]
    string Answer,
    [property: DataMember(Name = "upvotes")]
    int Upvotes,
    [property: DataMember(Name = "downvotes")]
    int Downvotes);