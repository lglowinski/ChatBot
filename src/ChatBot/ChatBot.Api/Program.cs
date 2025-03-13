using ChatBot.Api.Composition;
using ChatBot.Infrastructure;
using ChatBot.Common.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseDefaultServiceProvider((context, options) =>
{
    options.ValidateScopes = true;
    options.ValidateOnBuild = true;
});

using var loggerFactory =
    LoggerFactory.Create(loggingBuilder => loggingBuilder.SetMinimumLevel(LogLevel.Information).AddConsole());

builder.RegisterDependencies(loggerFactory.CreateLogger("Dependencies"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseAuthentication();
// app.UseAuthorization();
app.UseEndpoints<Program>();
app.UseHttpsRedirection();
app.UseCors();
app.UseRateLimiter();
app.AddInfrastructureMiddleware();

await app.RunAsync();

