namespace AspireOrchestrator.AppHost.RegistrationExtension;

public static class MigrationServiceRegistration
{
    public static IResourceBuilder<ProjectResource> AddMigrationService<TProject>(
        this IDistributedApplicationBuilder builder, 
        [ResourceName] string name = "migration",
        params IResourceBuilder<IResourceWithConnectionString>[] references)
        where TProject : IProjectMetadata, new()
    {
        var resource = builder
            .AddProject<TProject>(name);

        foreach (var reference in references)
        {
            resource.WithReference(reference).WaitFor(reference);
        }

        return resource;
    }
}