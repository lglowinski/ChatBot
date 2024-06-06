using AspireOrchestrator.ServiceDefaults;
using ChatBot.Infrastructure.Persistence;
using ChatBot.MigrationService;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Migrator>();
builder.Services.AddSingleton<SeedDataLoader>();
builder.AddServiceDefaults();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(Migrator.ActivitySourceName));

builder.AddSqlServerDbContext<QuestionDbContext>("avatarui");

var host = builder.Build();
host.Run();