using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;
using Microsoft.Extensions.Hosting;

namespace AspireOrchestrator.AppHost;

public static class ClientRegistration
{
    public static void RegisterChatBotFrontend(this IDistributedApplicationBuilder builder, IResourceBuilder<ProjectResource> apiReference)
    {
        var runCommand = ResolveRunCommand(builder.Environment);
        
        
        builder.AddNpmApp("chat-bot-frontend", "../src/Clients/chat-bot-frontend", runCommand)
            .WithReference(apiReference)
            .WithHttpEndpoint(env: "PORT")
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