using ErrorOr;
using MediatR;

namespace ChatBot.Application.Questions.Commands.LikeQuestionCommand;

public record LikeQuestionCommand(string Id, bool Liked, bool Disliked) : IRequest<ErrorOr<string>>;