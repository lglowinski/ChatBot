using MediatR;
using ErrorOr;

namespace ChatBot.Users.Application.Login.LoginQuery;

public record LoginQuery(string Email, string Password) : IRequest<ErrorOr<LoginResponse>>;