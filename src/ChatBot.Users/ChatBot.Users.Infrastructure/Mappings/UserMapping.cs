using ChatBot.Users.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace ChatBot.Users.Infrastructure.Mappings;

public static class UserMapping
{
    public static User ToDomain(this IdentityUser identityUser)
    {
        ArgumentNullException.ThrowIfNull(identityUser);
        ArgumentException.ThrowIfNullOrWhiteSpace(identityUser.Email);
        
        return new User(
            Guid.Parse(identityUser.Id),
            identityUser.Email
        );
    }

    public static IdentityUser ToIdentity(this User user)
    {
        ArgumentNullException.ThrowIfNull(user);
        
        return new IdentityUser
        {
            Id = user.Id.ToString(),
            Email = user.Email,
            UserName = user.Email,
            TwoFactorEnabled = user.IsTwoFactorEnabled
        };
    }
}