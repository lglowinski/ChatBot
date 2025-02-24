using ChatBot.Common.Endpoints;
using ChatBot.Users.Composition;

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
