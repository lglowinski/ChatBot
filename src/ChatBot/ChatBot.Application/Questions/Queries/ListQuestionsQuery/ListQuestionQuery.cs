using ChatBot.Application.Common;
using ErrorOr;
using MediatR;

namespace ChatBot.Application.Questions.Queries.ListQuestionsQuery;

public class ListQuestionQuery : IRequest<ErrorOr<List<QuestionSummary>>>, IOrderable
{
    public int Take { get; init; } = 10;
    public string? SearchTerm { get; init; }

    public string? OrderBy { get; set; }
    public int Page { get; set; } = 0;
}