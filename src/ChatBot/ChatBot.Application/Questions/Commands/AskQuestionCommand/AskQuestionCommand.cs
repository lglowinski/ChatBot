using ChatBot.Domain;
using ErrorOr;
using MediatR;

namespace ChatBot.Application.Questions.Commands.AskQuestionCommand;

public record AskQuestionCommand(string Question) : IRequest<ErrorOr<Question>>;