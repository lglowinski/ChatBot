using ChatBot.Domain;
using ErrorOr;
using MediatR;

namespace ChatBot.Application.Questions.Commands.GetQuestionQuery;

public class GetQuestionQuery(string id) : IRequest<ErrorOr<Question>>
{
    public string Id { get; init; } = id;
}