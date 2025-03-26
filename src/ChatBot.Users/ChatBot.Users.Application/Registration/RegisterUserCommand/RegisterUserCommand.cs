using MediatR;
using ErrorOr;

namespace ChatBot.Users.Application.Registration.RegisterUserCommand;

public record RegisterUserCommand(string Email, string Password) : IRequest<ErrorOr<RegistrationResult>>;