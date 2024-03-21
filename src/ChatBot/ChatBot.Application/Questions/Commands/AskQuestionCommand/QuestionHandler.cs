using ChatBot.Domain;
using ErrorOr;
using MediatR;

namespace ChatBot.Application.Questions.Commands.AskQuestionCommand;

public class QuestionHandler(IReasoningService reasoningService) : IRequestHandler<QuestionCommand, ErrorOr<Answer>>
{
    public async Task<ErrorOr<Answer>> Handle(QuestionCommand request, CancellationToken cancellationToken)
    {
        var question = new Question(request.Question);
        var answer = await reasoningService.AskQuestionAsync(question, cancellationToken);

        return new Answer(answer.TextValue);
    }
}