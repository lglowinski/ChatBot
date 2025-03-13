using ChatBot.Common.Communication;
using ChatBot.Common.Communication.Requests;
using MediatR;

namespace ChatBot.Users.Application.DeleteUser;

public record DeleteUserCommand(string Email) : IRequest<bool>;

public class DeleteUserCommandHandler(IUserManager userManager, ICommunication communication) : IRequestHandler<DeleteUserCommand, bool>
{
    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var result = await userManager.DeleteAsync(request.Email);
        if (result)
            await communication.SendAsync(new UserDeleted(request.Email), cancellationToken);
        
        return result;
    }
}