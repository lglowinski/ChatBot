using ChatBot.Common.Endpoints;
using ChatBot.Common.RateLimiting;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChatBot.Categories.Api.Endpoints;

public class CategoriesEndpoints : IEndpoints
{
    private const string ContentType = "application/json";
    private const string Tag = "Categories";
    private const string BaseRoute = "/api/categories";

    public static void DefineEndpoints(IEndpointRouteBuilder app, RateLimitingSettings rateLimitingSettings)
    {
        var builder = app.MapGroup(BaseRoute);

        builder.MapPost(string.Empty, AddCategoriesAsync)
            .WithName("AddCategories")
            .Accepts<string>(ContentType)
            .Produces<string>()
            .Produces(400)
            .Produces(503)
            .WithDescription("Creates new categories")
            .WithTags(Tag)
            .RequireRateLimiting(rateLimitingSettings.PolicyName)
            .AllowAnonymous();
        
        builder.MapGet("/{id}", GetCategoryAsync)
            .WithName("Register")
            .Accepts<string>(ContentType)
            .Produces<string>()
            .Produces(400)
            .Produces(503)
            .WithDescription("Registers user")
            .WithTags(Tag)
            .RequireRateLimiting(rateLimitingSettings.PolicyName)
            .AllowAnonymous();
        
        builder.MapDelete("/{id}", DeleteCategoryAsync)
            .WithName("Register")
            .Accepts<string>(ContentType)
            .Produces<string>()
            .Produces(400)
            .Produces(503)
            .WithDescription("Registers user")
            .WithTags(Tag)
            .RequireRateLimiting(rateLimitingSettings.PolicyName)
            .AllowAnonymous();
    }



    private static async Task<IResult> AddCategoriesAsync(string request, [FromServices] ISender sender,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    
    private static async Task<IResult> GetCategoryAsync(string request, [FromServices] ISender sender,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    
    private static async Task<IResult> DeleteCategoryAsync(string request, [FromServices] ISender sender,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public static void AddService(IServiceCollection services, IConfiguration configuration)
    {
    }
}