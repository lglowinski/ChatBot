using ChatBot.Common.Communication.Requests;

namespace ChatBot.Common.Communication.Serialization.Deserializers;

internal static class Deserializer
{
    private static readonly Dictionary<Type, Func<byte[], object>> Deserializers = new()
    {
        { typeof(UserDeleted), DeserializeUserDeleted },
        { typeof(QuestionDeleted), DeserializeQuestionDeleted }
    };
    
    internal static T Deserialize<T>(this ReadOnlySpan<byte> data)
    {
        return (T)Deserializers[typeof(T)](data.ToArray());
    }

    private static UserDeleted DeserializeUserDeleted(byte[] data)
    {
        return UserDeleted.Deserialize(data);
    }
    
    private static QuestionDeleted DeserializeQuestionDeleted(byte[] data)
    {
        return QuestionDeleted.Deserialize(data);
    }
}