using System.Runtime.Serialization;

namespace ChatBot.Contracts.Requests;

[DataContract]
public record GetQuestionDetailsRequest
{
    [DataMember(Name = "id")]
    public required string Id { get; set; }
}