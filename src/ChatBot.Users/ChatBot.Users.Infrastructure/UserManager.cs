using ChatBot.Common.TimeProvider;
using ChatBot.Users.Application;
using ChatBot.Users.Domain.Entities;
using ChatBot.Users.Infrastructure.Mappings;
using Microsoft.AspNetCore.Identity;
using ErrorOr;

namespace ChatBot.Users.Infrastructure;

public class UserManager(UserManager<IdentityUser> userManager, ITimeProvider timeProvider) : IUserManager
{
    
public async Task<ErrorOr<User>> CreateAsync(string email, string password)
    {
        var identityUser = new IdentityUser { UserName = email, Email = email, TwoFactorEnabled = true};
        var result = await userManager.CreateAsync(identityUser, password);

        if (result.Succeeded)
        {
            return identityUser.ToDomain();
        }

        return result.Errors.Select(e => Error.Validation(e.Code, e.Description)).ToList();
    }

    public async Task<string> GenerateTwoFactorSecretAsync(User user)
    {
        var identityUser = await userManager.FindByIdAsync(user.Id.ToString());
        
        var secretKey = await userManager.GetAuthenticatorKeyAsync(identityUser);
        if (string.IsNullOrEmpty(secretKey))
        {
            await userManager.ResetAuthenticatorKeyAsync(identityUser);
            secretKey = await userManager.GetAuthenticatorKeyAsync(identityUser);
        }

        return secretKey;
    }

    public string GenerateTwoFactorQrCodeUri(string email, string secretKey)
    {
        const string authenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";
        return string.Format(authenticatorUriFormat, 
                             Uri.EscapeDataString("avatarui"), 
                             Uri.EscapeDataString(email), 
                             secretKey);
    }

    public async Task<ErrorOr<bool>> VerifyTwoFactorTokenAsync(Guid userId, string token)
    {
        var identityUser = await userManager.FindByIdAsync(userId.ToString());
        if (identityUser == null)
        {
            return Error.NotFound("User not found.");
        }

        var isValid = await userManager.VerifyTwoFactorTokenAsync(identityUser, 
            userManager.Options.Tokens.AuthenticatorTokenProvider, 
                                                                   token);
        return isValid;
    }

    public async Task SetTwoFactorEnabledAsync(Guid userId, bool enabled)
    {
        var identityUser = await userManager.FindByIdAsync(userId.ToString());
        if (identityUser != null)
        {
            await userManager.SetTwoFactorEnabledAsync(identityUser, enabled);
        }
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        return user?.ToDomain();
    }

    public async Task<bool> VerifyLoginAsync(string email, string password)
    {
        var user = await userManager.FindByEmailAsync(email);
        
        if (user is null || user.LockoutEnd is not null || user.LockoutEnd > DateTimeOffset.Now)
            return false;
        
        var result = await userManager.CheckPasswordAsync(user, password);
        
        if(!result)
            await userManager.SetLockoutEndDateAsync(user, timeProvider.UtcNow.AddMinutes(5));

        return result;
    }
}