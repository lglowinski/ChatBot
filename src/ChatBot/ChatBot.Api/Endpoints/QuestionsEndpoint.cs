using ChatBot.Api.Endpoints.Internal;
using ChatBot.Api.Settings;
using ChatBot.Application.Questions.Commands.AskQuestionCommand;
using ChatBot.Application.Questions.Commands.GetQuestionQuery;
using ChatBot.Application.Questions.Queries.ListQuestionsQuery;
using ChatBot.Contracts.Requests;
using ChatBot.Contracts.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChatBot.Api.Endpoints;

public class AskQuestionsEndpoint : IEndpoints
{
    private const string ContentType = "application/json";
    private const string Tag = "Questions";
    private const string BaseRoute = "/api/questions";
    
    public static void DefineEndpoints(IEndpointRouteBuilder app, RateLimitingSettings rateLimitingSettings)
    {
        var builder = app.MapGroup(BaseRoute);

        builder.MapPost(string.Empty, AskQuestionAsync)
            .WithName("AskQuestion")
            .Accepts<CreateQuestionRequest>(ContentType)
            .WithDescription("Answers question defined by user")
            .Produces<CreateQuestionResponse>()
            .WithTags(Tag)
            .RequireRateLimiting(rateLimitingSettings.PolicyName)
            .AllowAnonymous();
        
        builder.MapGet("/{id}", GetQuestionDetailsAsync)
            .WithName("GetQuestionDetails")
            .WithDescription("Gets details of question")
            .Produces<GetQuestionDetailsResponse>()
            .WithTags(Tag)
            .AllowAnonymous();

        builder.MapGet(string.Empty, GetQuestionsAsync)
            .WithName("GetQuestions")
            .WithDescription("Get questions list based on filters")
            .Produces<ListQuestionsResponse>()
            .WithTags(Tag)
            .AllowAnonymous();
    }

    
    public static void AddService(IServiceCollection services, IConfiguration configuration)
    {
    }
    
    private static async Task<IResult> AskQuestionAsync(CreateQuestionRequest request,
        [FromServices] ISender sender,
        CancellationToken ct = default)
    {
        var result = await sender.Send(new AskQuestionCommand(request.Question), ct);

        return result.Match(
            question => Results.Created($"{BaseRoute}/{question.Id}",new CreateQuestionResponse(question.Id, question.Title, question.Answer, question.Upvotes, question.Downvotes)),
            error => Results.Problem(new ProblemDetails{Status = int.TryParse(error.First().Code, out var code) ? code : 400, Detail = error.First().Description})
        );
    }
    
    private static async Task<IResult> GetQuestionDetailsAsync([AsParameters] GetQuestionDetailsRequest request,[FromServices] ISender sender, CancellationToken ct = default)
    {
        var result = await sender.Send(new GetQuestionQuery(request.Id), ct);

        return result.Match(
            question => Results.Ok(new GetQuestionDetailsResponse(question.Title, question.Answer, question.Upvotes, question.Downvotes)),
            error => Results.Problem(new ProblemDetails{Status = int.TryParse(error.First().Code, out var code) ? code : 400, Detail = error.First().Description})
        );
    }
    
    private static async Task<IResult> GetQuestionsAsync([AsParameters] ListQuestionsRequest request, [FromServices] ISender sender, CancellationToken ct = default)
    {
        var result = await sender.Send(new ListQuestionQuery
        {
            Take = request.Take ?? 10,
            OrderBy = request.OrderBy,
            SearchTerm = request.SearchTerm
        }, ct);

        return result.Match(
            questions => Results.Ok(new ListQuestionsResponse(questions.Select(q => new Question(q.Id, q.Title, q.Summary, q.CreatedAt)))),
            Results.BadRequest);
    }
}