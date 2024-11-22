using ChatBot.Users.Application.TokenService;
using ErrorOr;
using MediatR;

namespace ChatBot.Users.Application.Verification.VerifyUserQuery;

public class VerifyUserQueryHandler(IUserManager userManager, ITokenService tokenService) : IRequestHandler<VerifyUserQuery, ErrorOr<VerificationResult>>
{
    public async Task<ErrorOr<VerificationResult>> Handle(VerifyUserQuery request, CancellationToken cancellationToken)
    {
        var user = await userManager.GetUserByEmailAsync(request.Email);

        if (user is null)
            return Error.NotFound("404", "User not found");
        
        var result = await userManager.VerifyTwoFactorTokenAsync(user.Id, request.Code);
        
        if(result.IsError)
            return result.Errors.ToList();
        
        if(!result.Value)
            return Error.NotFound("404", "User not found");

        var token = tokenService.GenerateAccessToken(user);

        return new VerificationResult(token, user);
    }
}