using ChatBot.Contracts.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChatBot.Api.Endpoints;

public static class QuestionsEndpoint
{
    public static IEndpointRouteBuilder AskQuestion(this IEndpointRouteBuilder endpoints)
    {
        var builder = endpoints.MapGroup("/api/questions");

        builder.MapPost("", async (QuestionRequest request, CancellationToken ct, [FromServices] ISender sender) =>
            {
                
            })
            .Accepts<QuestionRequest>("application/json")
            .WithDescription("Answers question defined by user")
            .AllowAnonymous();
        
        return endpoints;
    }
}