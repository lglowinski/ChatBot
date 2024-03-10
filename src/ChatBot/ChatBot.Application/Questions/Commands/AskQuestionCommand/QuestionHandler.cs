using ErrorOr;
using MediatR;

namespace ChatBot.Application.Questions.Commands.AskQuestionCommand;

public class QuestionHandler : IRequestHandler<QuestionCommand, ErrorOr<Answer>>
{
    public async Task<ErrorOr<Answer>> Handle(QuestionCommand request, CancellationToken cancellationToken)
    {
        return new Answer("Ok");
    }
}