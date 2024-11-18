using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ChatBot.Common.Auth;

public static class RegisterAuthentication
{
    public static void AddAuthentication(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.GetSection(nameof(JwtSettings)).Bind(jwtSettings);

        Validate(jwtSettings);
        
        serviceCollection.AddAuthentication(jwtSettings);
    }

    private static void Validate(JwtSettings jwtSettings)
    { 
      ArgumentNullException.ThrowIfNull(jwtSettings);
      ArgumentException.ThrowIfNullOrWhiteSpace(jwtSettings.Audience);
      ArgumentException.ThrowIfNullOrWhiteSpace(jwtSettings.Issuer);
      ArgumentException.ThrowIfNullOrWhiteSpace(jwtSettings.Key);
    }

    private static void AddAuthentication(this IServiceCollection serviceCollection, JwtSettings settings) =>
        serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = settings.Issuer,
                    ValidAudience = settings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Key))
                };
            });
}