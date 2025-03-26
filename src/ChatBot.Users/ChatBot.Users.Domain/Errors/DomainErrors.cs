using ErrorOr;
namespace ChatBot.Users.Domain.Errors;

public class DomainErrors
{
    public static class Authentication
    {
        public static Error InvalidCredentials =>
            Error.Validation(code: "Auth.InvalidCredentials", description: "Invalid credentials");
        
        public static Error InvalidTwoFactorAuthenticationCode =>
            Error.Validation(code: "Auth.InvalidTwoFactorAuthenticationCode ", description: "Invalid two-factor authentication code.");
    }
}