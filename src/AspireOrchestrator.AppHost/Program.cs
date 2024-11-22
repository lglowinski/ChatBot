using AspireOrchestrator.AppHost.RegistrationExtension;

var builder = DistributedApplication.CreateBuilder(args);

var questionDb = builder
    .AddSqlServer("questionsDb")
    .PublishAsAzureSqlDatabase()
    .AddDatabase("avatarui");

var usersDb = builder
    .AddSqlServer("usersDb")
    .PublishAsAzureSqlDatabase()
    .AddDatabase("users");

var kafka = builder
    .AddKafka("messaging")
    .WithKafkaUI()
    .PublishAsContainer();

var references = builder.RegisterApi(questionDb, usersDb, kafka);

builder
    .AddProject<Projects.ChatBot_MigrationService>("migration")
    .WithReference(questionDb).WithReference(usersDb).WaitFor(usersDb).WaitFor(questionDb);

builder.RegisterChatBotFrontend(references[typeof(Projects.ChatBot_Api)],
    references[typeof(Projects.ChatBot_Users_Api)]);

await builder
    .Build()
    .RunAsync();