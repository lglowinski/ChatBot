using ChatBot.Users.Application.Registration;
using ChatBot.Users.Domain.Entities;
using ErrorOr;

namespace ChatBot.Users.Application;

public interface IUserManager
{
    Task<ErrorOr<User>> CreateAsync(string email, string password);
    Task<string> GenerateTwoFactorSecretAsync(User user);
    string GenerateTwoFactorQrCodeUri(string email, string secretKey);
    Task<ErrorOr<bool>> VerifyTwoFactorTokenAsync(Guid userId, string token);
    Task SetTwoFactorEnabledAsync(Guid userId, bool enabled);
}