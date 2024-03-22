using ErrorOr;
using MediatR;

namespace ChatBot.Application.Questions.Commands.LikeQuestionCommand;

public class LikeQuestionCommandHandler(IQuestionsRepository repository) : IRequestHandler<LikeQuestionCommand, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handle(LikeQuestionCommand request, CancellationToken cancellationToken)
    {
        var question = await repository.GetQuestionByIdAsync(request.Id, cancellationToken);

        if (question is null)
            return Error.NotFound("404");
        
        if(request.Liked)
            question.Upvote();
        else if(request.Disliked)
            question.Downvote();
        
        await repository.UpdateQuestionAsync(question, cancellationToken);

        return question.Id;
    }
}