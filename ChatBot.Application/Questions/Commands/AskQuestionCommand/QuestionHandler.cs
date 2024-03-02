using MediatR;

namespace ChatBot.Application.Questions.AskQuestion;

public class QuestionHandler : IRequestHandler<QuestionCommand>
{
    public Task Handle(QuestionCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}