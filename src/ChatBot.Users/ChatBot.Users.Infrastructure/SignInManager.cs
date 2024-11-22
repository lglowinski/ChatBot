using ChatBot.Users.Application;
using ChatBot.Users.Application.Authentication;

namespace ChatBot.Users.Infrastructure;

public class SignInManager(IUserManager userManager) : ISignInManager
{
    public async Task<AuthenticationResult> PasswordSignInAsync(string email, string password)
    {
        var result = await userManager.VerifyLoginAsync(email, password);

        return new AuthenticationResult
        {
            Succeeded = result,
            IsLockedOut = !result,
            RequiresTwoFactor = true
        };
    }

    public Task<AuthenticationResult> TwoFactorAuthenticatorSignInAsync(string code, bool isPersistent, bool rememberClient)
    {
        throw new NotImplementedException();
    }
}