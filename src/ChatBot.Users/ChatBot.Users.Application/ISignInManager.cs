using ChatBot.Users.Application.Authentication;

namespace ChatBot.Users.Application;

public interface ISignInManager
{
    Task<AuthenticationResult> PasswordSignInAsync(string email, string password, bool isPersistent, bool lockoutOnFailure);
    Task<AuthenticationResult> TwoFactorAuthenticatorSignInAsync(string code, bool isPersistent, bool rememberClient);
}