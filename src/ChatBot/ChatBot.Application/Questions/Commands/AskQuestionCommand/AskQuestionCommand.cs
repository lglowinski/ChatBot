using ChatBot.Domain;
using ErrorOr;
using MediatR;

namespace ChatBot.Application.Questions.Commands.AskQuestionCommand;

public record AskQuestionCommand(string Question, string AuthorEmail) : IRequest<ErrorOr<Question>>;