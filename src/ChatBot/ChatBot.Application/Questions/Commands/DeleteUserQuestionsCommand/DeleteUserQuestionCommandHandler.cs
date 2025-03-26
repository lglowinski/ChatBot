using ChatBot.Common.Communication;
using ChatBot.Common.Communication.Requests;
using ErrorOr;
using MediatR;

namespace ChatBot.Application.Questions.Commands.DeleteUserQuestionsCommand;

public class DeleteUserQuestionCommandHandler(IQuestionsRepository questionsRepository, ICommunication communication) 
    : IRequestHandler<DeleteUserQuestionCommand, ErrorOr<DeleteUserQuestionResponse>>
{
    public async Task<ErrorOr<DeleteUserQuestionResponse>> Handle(DeleteUserQuestionCommand request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        var deletedQuestionsIds = await questionsRepository.DeleteUserQuestionsAsync(request.UserEmail, cancellationToken);

        try
        {
            foreach (var deletedQuestionId in deletedQuestionsIds)
            {
                await communication.SendAsync(new QuestionDeleted(deletedQuestionId), cancellationToken);
            }
        }
        catch (Exception e)
        {
            return Error.Failure();
        }

        return new DeleteUserQuestionResponse(deletedQuestionsIds);
    }
}