using ErrorOr;
using MediatR;

namespace ChatBot.Application.Questions.Commands.AskQuestionCommand;

public record QuestionCommand(string Question) : IRequest<ErrorOr<Answer>>;