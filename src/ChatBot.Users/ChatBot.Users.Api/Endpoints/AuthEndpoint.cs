using ChatBot.Common.Endpoints;
using ChatBot.Common.RateLimiting;
using ChatBot.Users.Application.Registration.RegisterUserCommand;
using ChatBot.Users.Library.Registration;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ChatBot.Users.Endpoints;

public class AuthEndpoint : IEndpoints
{
    private const string ContentType = "application/json";
    private const string Tag = "Users";
    private const string BaseRoute = "/api/users";
    
    public static void DefineEndpoints(IEndpointRouteBuilder app, RateLimitingSettings rateLimitingSettings)
    {
        var builder = app.MapGroup(BaseRoute);
        
        builder.MapPost("/register", RegisterAsync)
            .WithName("Register")
            .Accepts<RegisterRequest>(ContentType)
            .Produces<RegisterResponse>()
            .Produces(400)
            .Produces(503)
            .WithDescription("Registers user")
            .WithTags(Tag)
            .RequireRateLimiting(rateLimitingSettings.PolicyName)
            .AllowAnonymous();
        
        builder.MapPost("/verify", VerifyAsync)
            .WithName("Verify")
            .Accepts<RegisterRequest>(ContentType)
            .Produces<RegisterResponse>()
            .Produces(400)
            .Produces(503)
            .WithDescription("Verify MFA")
            .WithTags(Tag)
            .RequireRateLimiting(rateLimitingSettings.PolicyName)
            .AllowAnonymous();
        
        builder.MapPost("/login", LoginAsync)
            .WithName("Login")
            .Accepts<RegisterRequest>(ContentType)
            .Produces<RegisterResponse>()
            .Produces(400)
            .Produces(503)
            .WithDescription("Login")
            .WithTags(Tag)
            .RequireRateLimiting(rateLimitingSettings.PolicyName)
            .AllowAnonymous();
    }

    private static async Task<IResult> RegisterAsync(RegisterRequest request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new RegisterUserCommand(request.Email, request.Password), cancellationToken);

        return result.Match(
            registered =>
                Results.Ok(new RegisterResponse(registered.UserId, registered.Email, registered.SecretKey, registered.QrCodeUri)),
            error => Results.Problem(new ProblemDetails
            {
                Status = int.TryParse(error.First().Code, out var code)
                    ? code
                    : 400,
                Detail = error.First().Description
            }));
    }
    
    //TODO: Add verification of 2FA
    private static async Task<IResult> VerifyAsync(RegisterRequest request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new RegisterUserCommand(request.Email, request.Password), cancellationToken);

        return Results.Ok();
    }
    
    //TODO: Add login
    private static async Task<IResult> LoginAsync(RegisterRequest request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new RegisterUserCommand(request.Email, request.Password), cancellationToken);

        return Results.Ok();
    }
    
    public static void AddService(IServiceCollection services, IConfiguration configuration)
    {
    }
}