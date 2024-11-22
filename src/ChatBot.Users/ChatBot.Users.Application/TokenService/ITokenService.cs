using ChatBot.Users.Domain.Entities;

namespace ChatBot.Users.Application.TokenService;

public interface ITokenService
{
    string GenerateAccessToken(User user);
}