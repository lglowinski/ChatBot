using AspireOrchestrator.AppHost.RegistrationExtension;

var builder = DistributedApplication.CreateBuilder(args);

var db = builder
    .AddSqlServer("sql")
    .PublishAsAzureSqlDatabase()
    .AddDatabase("avatarui");

var api = builder.RegisterApi(db);

builder
     .AddProject<Projects.ChatBot_MigrationService>("migration")
     .WithReference(db);

builder.RegisterChatBotFrontend(api);

builder
    .Build()
    .Run();