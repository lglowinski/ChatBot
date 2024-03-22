using ErrorOr;

namespace ChatBot.Application.Common;

public class OrdererableVerifier : IVerifier<IOrderable>
{
    public ErrorOr<Success> Verify<TFor>(IOrderable verifable) where TFor : class
    {
        var properties = typeof(TFor).GetProperties().ToList();

        return properties.Exists(p => p.Name.Equals(verifable.OrderBy, StringComparison.OrdinalIgnoreCase))
            ? new Success()
            : Error.Validation("400");
    }
}