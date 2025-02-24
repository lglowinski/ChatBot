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

var categoriesDb = builder
    .AddSqlServer("categoriesDb")
    .PublishAsAzureSqlDatabase()
    .AddDatabase("categories");

var kafka = builder
    .AddKafka("messaging")
    .WithKafkaUI()
    .PublishAsContainer();

var migrator = builder
    .AddProject<Projects.ChatBot_MigrationService>("migration")
    .WithReference(questionDb).WithReference(usersDb).WithReference(categoriesDb).WaitFor(usersDb).WaitFor(questionDb).WaitFor(categoriesDb);


var references = builder.RegisterApi(questionDb, usersDb, categoriesDb, kafka, builder.Configuration, migrator);


builder.RegisterChatBotFrontend(references[typeof(Projects.ChatBot_Api)],
    references[typeof(Projects.ChatBot_Users_Api)]);

await builder
    .Build()
    .RunAsync();