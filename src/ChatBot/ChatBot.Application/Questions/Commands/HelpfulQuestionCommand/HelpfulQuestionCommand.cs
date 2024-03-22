using ErrorOr;
using MediatR;

namespace ChatBot.Application.Questions.Commands.HelpfulQuestionCommand;

public record HelpfulQuestionCommand(string Id) : IRequest<ErrorOr<Success>>;