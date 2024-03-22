using ErrorOr;
using MediatR;

namespace ChatBot.Application.Questions.Commands.HelpfulQuestionCommand;

public class HelpfulQuestionCommandHandler(IQuestionsRepository repository) : IRequestHandler<HelpfulQuestionCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(HelpfulQuestionCommand request, CancellationToken cancellationToken)
    {
        var question = await repository.GetQuestionByIdAsync(request.Id, cancellationToken);

        if (question is null)
            return Error.NotFound("404");
        
        question.Helped();
        
        await repository.UpdateQuestionAsync(question, cancellationToken);

        return new Success();
    }
}