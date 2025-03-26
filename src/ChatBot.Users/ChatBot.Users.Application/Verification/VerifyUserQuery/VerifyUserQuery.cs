using MediatR;
using ErrorOr;


namespace ChatBot.Users.Application.Verification.VerifyUserQuery;

public record VerifyUserQuery(string Email, string Code) : IRequest<ErrorOr<VerificationResult>>;