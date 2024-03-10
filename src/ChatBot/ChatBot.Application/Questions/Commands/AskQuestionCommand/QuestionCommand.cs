using ErrorOr;
using MediatR;

namespace ChatBot.Application.Questions.Commands.AskQuestionCommand;

public class QuestionCommand(string question) : IRequest<ErrorOr<Answer>>
{
}