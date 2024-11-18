using AspireOrchestrator.ServiceDefaults;
using ChatBot.Infrastructure.Persistence;
using ChatBot.MigrationService;
using ChatBot.Users.Infrastructure.Persistance;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<QuestionMigrator>();
builder.Services.AddHostedService<UsersMigrator>();
builder.Services.AddSingleton<SeedDataLoader>();
builder.AddServiceDefaults();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(QuestionMigrator.ActivitySourceName));

builder.AddSqlServerDbContext<QuestionDbContext>("avatarui");
builder.AddSqlServerDbContext<UsersDbContext>("users");

var host = builder.Build();
host.Run();