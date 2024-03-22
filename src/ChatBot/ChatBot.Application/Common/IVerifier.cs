using ErrorOr;

namespace ChatBot.Application.Common;

public interface IVerifier<in TVerifable>
{
    public ErrorOr<Success> Verify<TFor>(TVerifable verifable) where TFor : class;
}