using ErrorOr;
using MediatR;

namespace ChatBot.Application.Questions.Commands.DeleteUserQuestionsCommand;

public record DeleteUserQuestionCommand(string UserEmail) : IRequest<ErrorOr<DeleteUserQuestionResponse>>;