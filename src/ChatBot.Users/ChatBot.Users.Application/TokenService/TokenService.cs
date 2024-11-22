using System.Collections.Immutable;
using System.Security.Claims;
using System.Text;
using ChatBot.Common.Auth;
using ChatBot.Common.TimeProvider;
using ChatBot.Users.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace ChatBot.Users.Application.TokenService;

public class TokenService(JwtSettings settings, ITimeProvider timeProvider) : ITokenService
{
    public string GenerateAccessToken(User user)
    {
        var utcNow = DateTime.UtcNow;
        var claims = new Dictionary<string, object>
        {
            [ClaimTypes.NameIdentifier] = user.Id.ToString(),
            [ClaimTypes.Email] = user.Email,
            [JwtRegisteredClaimNames.Jti] = Guid.NewGuid().ToString(),
            [JwtRegisteredClaimNames.Iat] = EpochTime.GetIntDate(utcNow)
        }.ToImmutableDictionary();
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Key));
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Claims = claims,
            IssuedAt = utcNow,
            NotBefore = utcNow,
            Expires = utcNow.AddMinutes(30),
            Issuer = settings.Issuer,
            Audience = settings.Audience,
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        };
        
        var handler = new Microsoft.IdentityModel.JsonWebTokens.JsonWebTokenHandler
        {
            SetDefaultTimesOnTokenCreation = false
        };

        return handler.CreateToken(tokenDescriptor);
    }
}