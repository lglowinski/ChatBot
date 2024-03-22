using ChatBot.Domain;
using ErrorOr;
using MediatR;

namespace ChatBot.Application.Questions.Commands.GetQuestionQuery;

public class GetQuestionQueryHandler(IQuestionsRepository repository) : IRequestHandler<GetQuestionQuery, ErrorOr<Question>>
{
    public async Task<ErrorOr<Question>> Handle(GetQuestionQuery request, CancellationToken cancellationToken)
    {
        var question = await repository.GetQuestionByIdAsync(request.Id, cancellationToken);

        if (question is null)
            return Error.NotFound(code:"404", description: "Question not found");
        
        return question;
    }
}