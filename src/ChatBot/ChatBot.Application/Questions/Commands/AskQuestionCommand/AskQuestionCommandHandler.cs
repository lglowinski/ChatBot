using ChatBot.Domain;
using ErrorOr;
using MediatR;

namespace ChatBot.Application.Questions.Commands.AskQuestionCommand;

public class AskQuestionCommandHandler(IReasoningService reasoningService, 
    IQuestionsRepository questionsRepository, 
    ITimeProvider timeProvider) : IRequestHandler<AskQuestionCommand, ErrorOr<Question>>
{
    public async Task<ErrorOr<Question>> Handle(AskQuestionCommand request, CancellationToken cancellationToken)
    {
        var answer = await reasoningService.AskQuestionAsync(request.Question, cancellationToken);

        var question = new Question(Guid.NewGuid().ToString(), request.Question, answer.Value, answer.Tags.ToList(),
            timeProvider.UtcNow, answer.Summary);
        
        await questionsRepository.AddQuestionAsync(question, cancellationToken);

        return question;
    }
}