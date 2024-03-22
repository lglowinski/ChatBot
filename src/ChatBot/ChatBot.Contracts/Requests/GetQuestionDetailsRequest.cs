using System.Runtime.Serialization;

namespace ChatBot.Contracts.Requests;

[DataContract]
public record GetQuestionDetailsRequest
{
    public required string Id { get; set; }
}