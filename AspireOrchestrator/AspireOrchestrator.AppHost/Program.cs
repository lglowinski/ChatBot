using AspireOrchestrator.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

var db = builder
    .AddSqlServer("sql")
    .AddDatabase("avatarui");

var api = builder
    .AddProject<Projects.ChatBot_Api>("api")
    .WithReference(db);

builder
    .AddProject<Projects.ChatBot_MigrationService>("migration")
    .WithReference(db);

builder.RegisterChatBotFrontend(api);

builder
    .Build()
    .Run();