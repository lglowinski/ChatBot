using ChatBot.Users.Application;
using ChatBot.Users.Application.Authentication;

namespace ChatBot.Users.Infrastructure;

public class SignInManager : ISignInManager
{
    public Task<AuthenticationResult> PasswordSignInAsync(string email, string password, bool isPersistent, bool lockoutOnFailure)
    {
        throw new NotImplementedException();
    }

    public Task<AuthenticationResult> TwoFactorAuthenticatorSignInAsync(string code, bool isPersistent, bool rememberClient)
    {
        throw new NotImplementedException();
    }
}