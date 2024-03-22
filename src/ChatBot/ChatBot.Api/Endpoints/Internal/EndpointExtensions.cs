using System.Reflection;
using ChatBot.Api.Settings;

namespace ChatBot.Api.Endpoints.Internal;

public static class EndpointExtensions
{
    public static void AddEndpoints<TTypeMarker>(this IServiceCollection services, IConfiguration configuration)
    {
        AddEndpoints(services, typeof(TTypeMarker), configuration);
    }

    public static void AddEndpoints(this IServiceCollection services,
        Type typeMarker,
        IConfiguration configuration)
    {
        var endpointTypes = GetEndpointsFromContainingAssembly(typeMarker);

        foreach (var endpointType in endpointTypes)
        {
            endpointType.GetMethod(nameof(IEndpoints.AddService))!.Invoke(null, [services, configuration]);
        }
    }

    public static void UseEndpoints<TTypeMarker>(this IApplicationBuilder app)
    {
        UseEndpoints(app, typeof(TTypeMarker));
    }

    public static void UseEndpoints(this IApplicationBuilder app, Type typeMarker)
    {
        var endpointTypes = GetEndpointsFromContainingAssembly(typeMarker);

        foreach (var endpointType in endpointTypes)
        {
            endpointType.GetMethod(nameof(IEndpoints.DefineEndpoints))!.Invoke(null,
                [app, app.ApplicationServices.GetRequiredService<RateLimitingSettings>()]);
        }
    }

    private static IEnumerable<TypeInfo> GetEndpointsFromContainingAssembly(Type typeMarker)
    {
        return typeMarker
            .Assembly
            .DefinedTypes
            .Where(x => x is { IsAbstract: false, IsInterface: false } && x.IsAssignableTo(typeof(IEndpoints)));
    }
}