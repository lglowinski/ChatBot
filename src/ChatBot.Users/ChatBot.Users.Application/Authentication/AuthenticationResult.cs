namespace ChatBot.Users.Application.Authentication;

public class AuthenticationResult
{
    public bool Succeeded { get; init; }
    public bool RequiresTwoFactor { get; init; }
    public bool IsLockedOut { get; init; }
    public IEnumerable<string> Errors { get; init; } = Enumerable.Empty<string>();

    public static AuthenticationResult Success() => new() { Succeeded = true };
    public static AuthenticationResult Failure(IEnumerable<string> errors) => new() { Succeeded = false, Errors = errors };
    public static AuthenticationResult TwoFactorRequired() => new() { RequiresTwoFactor = true };
    public static AuthenticationResult LockedOut() => new() { IsLockedOut = true };
}