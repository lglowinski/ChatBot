using AspireOrchestrator.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

var api = builder
    .AddProject<Projects.ChatBot_Api>("api");

builder.RegisterChatBotFrontend(api);

builder
    .Build()
    .Run();