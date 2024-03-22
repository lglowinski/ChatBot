using AspireOrchestrator.ServiceDefaults;
using ChatBot.Api.Endpoints;
using ChatBot.Api.Endpoints.Internal;
using ChatBot.Api.Settings;
using ChatBot.Application;
using ChatBot.Infrastructure;
using ChatBot.OpenAiFacade;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.AddServiceDefaults();
builder.RegisterInfrastructure("avatarui");

var openAiSettings = new OpenAiSettings();

builder.Configuration.GetSection(nameof(OpenAiSettings)).Bind(openAiSettings);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(p =>
        p
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});
builder.Services.AddApplication();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpoints<Program>(builder.Configuration);
builder.Services.AddOpenAiClient(openAiSettings.Url, openAiSettings.ApiKey);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseEndpoints<Program>();
app.UseHttpsRedirection();
app.UseCors();

app.Run();

