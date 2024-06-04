namespace AspireOrchestrator.AppHost.RegistrationExtension;

public static class ApiRegistration
{
    public static IResourceBuilder<ProjectResource> RegisterApi(this IDistributedApplicationBuilder builder,
        IResourceBuilder<SqlServerDatabaseResource> db)
    {
        var envVariables = RegisterEnvVariables(builder);
        
        var api = builder
            .AddProject<Projects.ChatBot_Api>("api")
            .WithReference(db);

        foreach (var variable in envVariables)
            api.WithEnvironment(variable.Key, variable.Value);

        return api;
    }

    private static IEnumerable<(string Key, IResourceBuilder<ParameterResource> Value)> RegisterEnvVariables(IDistributedApplicationBuilder builder)
    {
        var url = builder.AddParameter("apiUrl", secret: true);
        var key = builder.AddParameter("key", secret: true);

        yield return new ValueTuple<string, IResourceBuilder<ParameterResource>>("OpenAiSettings__Url", url);
        yield return new ValueTuple<string, IResourceBuilder<ParameterResource>>("OpenAiSettings__ApiKey", key);
    }
}