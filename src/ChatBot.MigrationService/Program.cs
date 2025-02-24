using AspireOrchestrator.ServiceDefaults;
using ChatBot.Categories.Infrastructure.Persistance;
using ChatBot.Infrastructure.Persistence;
using ChatBot.MigrationService;
using ChatBot.MigrationService.Migrations;
using ChatBot.Users.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore.Migrations;

var builder = Host.CreateApplicationBuilder(args);
builder.AddSqlServerDbContext<QuestionDbContext>("avatarui");
builder.AddSqlServerDbContext<UsersDbContext>("users");
builder.AddSqlServerDbContext<CategoriesDbContext>("categories");
builder.Services.AddSingleton<IMigration, Migration<QuestionDbContext>>();
builder.Services.AddSingleton<IMigration, Migration<UsersDbContext>>();
builder.Services.AddSingleton<IMigration, Migration<CategoriesDbContext>>();

builder.Services.AddHostedService<MigrationService>();

builder.Services.AddSingleton<SeedDataLoader>();
builder.AddServiceDefaults();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(MigrationConsts.ActivitySourceName));

var host = builder.Build();
await host.RunAsync();