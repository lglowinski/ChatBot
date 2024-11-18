using MediatR;
using ErrorOr;

namespace ChatBot.Users.Application.Registration.RegisterUserCommand;

public class RegisterUserCommandHandler(IUserManager userManager) : IRequestHandler<RegisterUserCommand, ErrorOr<RegistrationResult>>
{
    public async Task<ErrorOr<RegistrationResult>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var result = await userManager.CreateAsync(request.Email, request.Password);
        
        if(result.IsError)
        {
            return result.Errors.ToList();
        }

        var user = result.Value;
        var secretKey = await userManager.GenerateTwoFactorSecretAsync(user);
        var qrCodeUri = userManager.GenerateTwoFactorQrCodeUri(user.Email, secretKey);
        
        return new RegistrationResult(user.Id, user.Email, secretKey, qrCodeUri);
    }
}