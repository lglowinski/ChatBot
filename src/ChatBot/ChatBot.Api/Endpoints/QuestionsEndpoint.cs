using ChatBot.Application.Questions.Commands.AskQuestionCommand;
using ChatBot.Contracts.Requests;
using ChatBot.Contracts.Responses;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ChatBot.Api.Endpoints;

public class QuestionsEndpoint : IEndpoints
{
    private const string ContentType = "application/json";
    private const string Tag = "Questions";
    private const string BaseRoute = "/api/questions";
    
    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        var builder = app.MapGroup(BaseRoute);

        builder.MapPost(string.Empty, AskQuestionAsync)
            .WithName("AskQuestion")
            .Accepts<QuestionRequest>(ContentType)
            .WithDescription("Answers question defined by user")
            .Produces<QuestionResponse>()
            .WithTags(Tag)
            .AllowAnonymous();
    }
    
    public static void AddService(IServiceCollection services, IConfiguration configuration)
    {
    }
    
    private static async Task<IResult> AskQuestionAsync(QuestionRequest request,
        [FromServices] ISender sender,
        CancellationToken ct = default)
    {
        var result = await sender.Send(new QuestionCommand(request.Question), ct);

        return result.Match(
            answer => Results.Ok(new QuestionResponse(answer.Value, Animation.None)),
            _ => Results.BadRequest("Error while processing question")
        );
    }
}