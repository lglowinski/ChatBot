using AspireOrchestrator.ServiceDefaults;
using ChatBot.Categories.Api.Composition;
using ChatBot.Common.Communication;
using ChatBot.Common.Communication.Configuration;
using ChatBot.Common.Endpoints;
using ChatBot.Common.RateLimiting;
using ChatBot.Common.TimeProvider;

var builder = WebApplication.CreateBuilder(args);
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

app.UseEndpoints<Program>();
app.UseRateLimiter();
app.UseHttpsRedirection();

await app.RunAsync();