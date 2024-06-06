using Microsoft.Extensions.Hosting;

namespace AspireOrchestrator.AppHost.RegistrationExtension;

public static class ClientRegistration
{
    public static void RegisterChatBotFrontend(this IDistributedApplicationBuilder builder, IResourceBuilder<ProjectResource> apiReference)
    {
        var runCommand = ResolveRunCommand(builder.Environment);
        
        
        builder.AddNpmApp("chat-bot-frontend", "../Clients/chat-bot-frontend", runCommand)
            .WithReference(apiReference)
            .WithHttpEndpoint(env: "PORT", targetPort:4173)
            .WithExternalHttpEndpoints()
            .PublishAsDockerFile();
    }

    private static string ResolveRunCommand(IHostEnvironment builderEnvironment)
    {
        var runCommand = "dev";
        
        if(builderEnvironment.IsProduction())
        {
            runCommand = "dev"; //TODO : Adjust for production
        }

        return runCommand;
    }
}