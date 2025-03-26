using MediatR;
using ErrorOr;

namespace ChatBot.Users.Application.Login.LoginQuery;

public class LoginQueryHandler(ISignInManager signInManager) : IRequestHandler<LoginQuery, ErrorOr<LoginResponse>>
{
    public async Task<ErrorOr<LoginResponse>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var result = await signInManager.PasswordSignInAsync(request.Email, request.Password);
        
        if(!result.Succeeded)
            return Error.NotFound("404", "User not found");

        return new LoginResponse(request.Email);
    }
}