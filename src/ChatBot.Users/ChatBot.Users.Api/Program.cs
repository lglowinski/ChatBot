using AspireOrchestrator.ServiceDefaults;
using ChatBot.Common.Auth;
using ChatBot.Common.Endpoints;
using ChatBot.Common.RateLimiting;
using ChatBot.Common.TimeProvider;
using ChatBot.Users.Application;
using ChatBot.Users.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDefaultTimeProvider();
builder.Services.AddEndpoints<Program>(builder.Configuration);
builder.Services.AddJwtSettings(builder.Configuration);
var rateLimitingSettings = new RateLimitingSettings();
builder.Configuration.GetSection(nameof(RateLimitingSettings)).Bind(rateLimitingSettings);
        
builder.Services.AddSingleton(rateLimitingSettings);

builder.Services.AddRateLimiting(rateLimitingSettings);

builder.Services.AddApplication();
builder.AddInfrastructure("users");

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
