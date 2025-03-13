using Microsoft.Extensions.Configuration;

namespace AspireOrchestrator.AppHost.RegistrationExtension;

public static class ApiRegistration
{
    public static Dictionary<Type, IResourceBuilder<ProjectResource>> RegisterApi(
        this IDistributedApplicationBuilder builder,
        IResourceBuilder<SqlServerDatabaseResource> questionsDb,
        IResourceBuilder<SqlServerDatabaseResource> usersDb,
        IResourceBuilder<SqlServerDatabaseResource> categoriesDb,
        IResourceBuilder<KafkaServerResource> kafka, IResourceBuilder<ProjectResource> migrator)
    {
        var envVariables = RegisterEnvVariables(builder);
        var authVariables = RegisterAuthVariables(builder);

        var useKafka = builder.AddParameter("useKafka");
        
        var api = builder
            .AddProject<Projects.ChatBot_Api>("api")
            .WithReference(questionsDb)
            .WithReference(kafka)
            .WaitFor(questionsDb)
            .WaitForCompletion(migrator)
            .WithEnvironment("UseKafka", useKafka);

        var usersApi = builder
            .AddProject<Projects.ChatBot_Users_Api>("usersApi")
            .WithReference(usersDb)
            .WithReference(kafka)
            .WaitFor(usersDb)
            .WaitForCompletion(migrator)
            .WithEnvironment("UseKafka", useKafka);

        var categoriesApi = builder
            .AddProject<Projects.ChatBot_Categories_Api>("categoriesApi")
            .WithReference(categoriesDb)
            .WithReference(kafka).WaitForCompletion(migrator).WaitFor(categoriesDb);

        foreach (var variable in envVariables)
        {
            api.WithEnvironment(variable.Key, variable.Value);
        }

        foreach (var variable in authVariables)
        {
            api.WithEnvironment(variable.Key, variable.Value);
            usersApi.WithEnvironment(variable.Key, variable.Value);
        }

        return new Dictionary<Type, IResourceBuilder<ProjectResource>>
        {
            { typeof(Projects.ChatBot_Api), api },
            { typeof(Projects.ChatBot_Users_Api), usersApi },
            { typeof(Projects.ChatBot_Categories_Api), categoriesApi }
        };
    }

    private static IEnumerable<(string Key, IResourceBuilder<ParameterResource> Value)> RegisterEnvVariables(
        IDistributedApplicationBuilder builder)
    {
        var url = builder.AddParameter("apiUrl", secret: true);
        var key = builder.AddParameter("key", secret: true);

        yield return new ValueTuple<string, IResourceBuilder<ParameterResource>>("OpenAiSettings__Url", url);
        yield return new ValueTuple<string, IResourceBuilder<ParameterResource>>("OpenAiSettings__ApiKey", key);
    }

    private static IEnumerable<(string Key, IResourceBuilder<ParameterResource> Value)> RegisterAuthVariables(
        IDistributedApplicationBuilder builder)
    {
        var issuer = builder.AddParameter("issuer", secret: true);
        var audience = builder.AddParameter("audience", secret: true);
        var key = builder.AddParameter("authKey", secret: true);

        yield return new ValueTuple<string, IResourceBuilder<ParameterResource>>("JwtSettings__Issuer", issuer);
        yield return new ValueTuple<string, IResourceBuilder<ParameterResource>>("JwtSettings__Audience", audience);
        yield return new ValueTuple<string, IResourceBuilder<ParameterResource>>("JwtSettings__Key", key);
    }
}