using System.Runtime.Serialization;

namespace ChatBot.Contracts.Responses;

[DataContract]
public class Answer
{
    [DataMember(Name = "text")]
    public string Text { get; set; }
    [DataMember(Name = "animation")]
    public Animation Animation { get; set; }
}

public enum Animation
{
    None = 0,
    Thinking = 1,
    Happy = 2,
    Sad = 3,
    Angry = 4,
    Confused = 5
}