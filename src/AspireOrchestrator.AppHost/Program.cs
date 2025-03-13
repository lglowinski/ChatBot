using AspireOrchestrator.AppHost.RegistrationExtension;
using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Microsoft.Extensions.Logging;

var builder = DistributedApplication.CreateBuilder(args);

var logger = LoggerFactory.Create((loggerBuilder) => loggerBuilder.AddConsole()).CreateLogger<Program>();

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
    .WithReference(questionDb)
    .WithReference(usersDb)
    .WithReference(categoriesDb)
    .WaitFor(usersDb).WaitFor(questionDb)
    .WaitFor(categoriesDb);


var references = builder.RegisterApi(questionDb, usersDb, categoriesDb, kafka, migrator);

builder.RegisterChatBotFrontend(references[typeof(Projects.ChatBot_Api)],
    references[typeof(Projects.ChatBot_Users_Api)]);

builder.Eventing.Subscribe<ResourceReadyEvent>(kafka.Resource, async (@event, token) =>
{
    string[] topics = ["userDeleted", "questionDeleted"];
    using (var adminClient = new AdminClientBuilder(new AdminClientConfig
           {
               BootstrapServers = await kafka.Resource.ConnectionStringExpression.GetValueAsync(token)
           }).Build())
    {
        await adminClient.CreateTopicsAsync(topics.Select(topic =>
            new TopicSpecification
            {
                Name = topic,
                ReplicationFactor = 1,
                NumPartitions = 1
            }));
    }
});

await builder
.Build()
.RunAsync();