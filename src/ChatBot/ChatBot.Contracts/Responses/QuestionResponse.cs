using System.Runtime.Serialization;

namespace ChatBot.Contracts.Responses;

[DataContract]
public class QuestionResponse(string text, Animation animation)
{
    [DataMember(Name = "text")]
    public string Text { get; } = text;

    [DataMember(Name = "animation")]
    public Animation Animation { get; } = animation;
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