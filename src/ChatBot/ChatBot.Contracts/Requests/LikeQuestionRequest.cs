using System.Runtime.Serialization;

namespace ChatBot.Contracts.Requests;

[DataContract]
public class LikeQuestionRequest
{
    [DataMember(Name = "liked")]
    public bool Liked { get; set; }
    [DataMember(Name = "disliked")]
    public bool Disliked { get; set; }
}