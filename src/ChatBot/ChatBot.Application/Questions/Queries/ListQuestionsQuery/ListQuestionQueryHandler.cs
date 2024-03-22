using ChatBot.Application.Common;
using ChatBot.Domain;
using ErrorOr;
using MediatR;

namespace ChatBot.Application.Questions.Queries.ListQuestionsQuery;

public class ListQuestionQueryHandler(IQuestionsRepository repository, IVerifier<IOrderable> verifier) : IRequestHandler<ListQuestionQuery, ErrorOr<List<QuestionSummary>>>
{
    public async Task<ErrorOr<List<QuestionSummary>>> Handle(ListQuestionQuery request, CancellationToken cancellationToken)
    {
        var verificationResult = verifier.Verify<Question>(request);
        if (verificationResult.IsError)
            return verificationResult.FirstError;
        
        var questions =
            await repository.SearchQuestions(request.SearchTerm, request.OrderBy, request.Take, request.Page, cancellationToken);

        return questions.Select(ConvertToSummary).ToList();
    }

    private static QuestionSummary ConvertToSummary(Question question) 
        => new(question.Id, question.Title, question.CreatedAt.DateTime, question.Summary);
}